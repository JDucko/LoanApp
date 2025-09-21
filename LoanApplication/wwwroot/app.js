const apiBase = window.location.origin + '/api';

async function fetchJson(url, opts) {
  const res = await fetch(url, opts);
  if (!res.ok) {
    const text = await res.text();
    throw new Error(text || res.statusText);
  }
  return res.json();
}

// Load users into dropdowns
async function loadUsers() {
  try {
    const users = await fetchJson(`${apiBase}/User`);
    const loanUserId = document.getElementById('loanUserId');
    const loansUserId = document.getElementById('loansUserId');
    loanUserId.innerHTML = '';
    loansUserId.innerHTML = '';
    users.forEach(u => {
      const opt = document.createElement('option');
      opt.value = u.id;
      opt.textContent = `${u.firstName} ${u.lastName}`;
      loanUserId.appendChild(opt);
      loansUserId.appendChild(opt.cloneNode(true));
    });
    // Enable or disable loan controls depending on whether users exist
    setLoanControlsEnabled(users && users.length > 0);
  } catch (err) {
    console.error('Failed to load users', err);
  }
}

// Create user
document.getElementById('createUserForm').addEventListener('submit', async (e) => {
  e.preventDefault();
  const firstName = document.getElementById('firstName').value.trim();
  const lastName = document.getElementById('lastName').value.trim();
  const resultDiv = document.getElementById('createUserResult');
  resultDiv.textContent = 'Creating...';
  try {
    const user = await fetchJson(`${apiBase}/User`, {
      method: 'POST',
      headers: {'Content-Type':'application/json'},
      body: JSON.stringify({ firstName, lastName })
    });
    resultDiv.textContent = `Created user ${user.id}: ${user.firstName} ${user.lastName}`;
    // reload users
    await loadUsers();
    // select the newly created user in loan selectors
    const loanUserId = document.getElementById('loanUserId');
    const loansUserId = document.getElementById('loansUserId');
    if (loanUserId) loanUserId.value = user.id;
    if (loansUserId) loansUserId.value = user.id;
    document.getElementById('createUserForm').reset();
  } catch (err) {
    resultDiv.textContent = 'Error: ' + err.message;
  }
});

function setLoanControlsEnabled(enabled) {
  const notice = document.getElementById('userRequiredNotice');
  const createLoanForm = document.getElementById('createLoanForm');
  const loadLoansBtn = document.getElementById('loadLoans');
  if (!enabled) {
    notice.style.display = 'block';
    createLoanForm.classList.add('disabled');
    loadLoansBtn.disabled = true;
  } else {
    notice.style.display = 'none';
    createLoanForm.classList.remove('disabled');
    loadLoansBtn.disabled = false;
  }
}

// Create loan
document.getElementById('createLoanForm').addEventListener('submit', async (e) => {
  e.preventDefault();
  const userId = Number(document.getElementById('loanUserId').value);
  const amount = Number(document.getElementById('amount').value);
  const annualInterestRate = Number(document.getElementById('annualInterestRate').value);
  const loanTerm = Number(document.getElementById('loanTerm').value);
  const frequency = Number(document.getElementById('frequency').value);
  const resultDiv = document.getElementById('createLoanResult');
  resultDiv.textContent = 'Creating loan...';
  try {
    const loan = await fetchJson(`${apiBase}/Loan`, {
      method: 'POST',
      headers: {'Content-Type':'application/json'},
      body: JSON.stringify({ userId, amount, annualInterestRate, loanTermInMonths: loanTerm, frequency })
    });
    resultDiv.textContent = `Created loan ${loan.id} for user ${loan.userId}`;
    document.getElementById('createLoanForm').reset();
  } catch (err) {
    resultDiv.textContent = 'Error: ' + err.message;
  }
});

// Load loans for a user
document.getElementById('loadLoans').addEventListener('click', async () => {
  const userId = Number(document.getElementById('loansUserId').value);
  const listDiv = document.getElementById('loansList');
  listDiv.innerHTML = 'Loading...';
  try {
    const loans = await fetchJson(`${apiBase}/Loan/user/${userId}`);
    if (!loans || loans.length === 0) {
      listDiv.textContent = 'No loans found';
      return;
    }
    listDiv.innerHTML = '';
    loans.forEach(loan => {
      const div = document.createElement('div');
      div.className = 'loan';
        div.innerHTML = `<div style="display:flex;justify-content:space-between;align-items:center"><strong>Loan #${loan.id}</strong> <small>Amount: ${loan.amount} | Rate: ${loan.annualInterestRate}% | Term: ${loan.loanTermInMonths} months</small></div>`;
        const viewBtn = document.createElement('button');
        viewBtn.textContent = 'View Schedule';
        viewBtn.style.marginTop = '8px';
        viewBtn.addEventListener('click', () => showSchedule(loan.id));
        div.appendChild(viewBtn);
      if (loan.loanSchedules && loan.loanSchedules.length) {
        const ul = document.createElement('ul');
        loan.loanSchedules.forEach(s => {
          const li = document.createElement('li');
          li.textContent = `${new Date(s.dueDate).toLocaleDateString()}: ${s.amountDue} (${s.balanceAfterPayment})`;
          ul.appendChild(li);
        });
        div.appendChild(ul);
      }
      listDiv.appendChild(div);
    });
  } catch (err) {
    listDiv.textContent = 'Error: ' + err.message;
  }
});

// initial load
loadUsers();

// Schedule modal logic
const scheduleModal = document.getElementById('scheduleModal');
const scheduleBody = document.getElementById('scheduleBody');
const scheduleTitle = document.getElementById('scheduleTitle');
document.getElementById('closeSchedule').addEventListener('click', () => closeSchedule());

function openSchedule() {
  scheduleModal.setAttribute('aria-hidden', 'false');
}
function closeSchedule() {
  scheduleModal.setAttribute('aria-hidden', 'true');
  scheduleBody.innerHTML = '';
  scheduleTitle.textContent = 'Loan Schedule';
}

async function showSchedule(loanId) {
  scheduleBody.innerHTML = 'Loading...';
  openSchedule();
  try {
    const loan = await fetchJson(`${apiBase}/Loan/${loanId}`);
    scheduleTitle.textContent = `Loan #${loan.id} Schedule`;
    const schedules = loan.loanSchedules || [];
    if (schedules.length === 0) {
      scheduleBody.innerHTML = '<div>No schedule available</div>';
      return;
    }
    const table = document.createElement('table');
    table.className = 'schedule-table';
    table.innerHTML = '<thead><tr><th>#</th><th>Due Date</th><th>Amount Due</th><th>Principal</th><th>Interest</th><th>Balance</th></tr></thead>';
    const tbody = document.createElement('tbody');
    schedules.forEach((s, i) => {
      const tr = document.createElement('tr');
      tr.innerHTML = `<td>${i+1}</td><td>${new Date(s.dueDate).toLocaleDateString()}</td><td>${s.amountDue}</td><td>${s.principal}</td><td>${s.interest}</td><td>${s.balanceAfterPayment}</td>`;
      tbody.appendChild(tr);
    });
    table.appendChild(tbody);
    scheduleBody.innerHTML = '';
    scheduleBody.appendChild(table);
  } catch (err) {
    scheduleBody.innerHTML = 'Error: ' + err.message;
  }
}
