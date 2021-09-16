// Enter JavaScript for the exercise here...
//! Final Updated

const form = document.querySelector('form');
const tbody = document.querySelector('tbody');
const debitDisplay = document.querySelector('.total.debits');
const creditDisplay = document.querySelector('.total.credits');
const formatter = new Intl.NumberFormat('en-Us', {
    style: 'currency',
    currency: 'USD',
});
let debitTotal = 0;
let creditTotal = 0;
let timer;



// Function for Validation
form.addEventListener('submit', function (evt) {
    // Declare input value
    let description = evt.target.elements['description'].value;
    let option = evt.target.elements['type'].value;
    let currency = evt.target.elements['currency'].value;
    let amount = formatter.format(currency);
    // Declare error 
    const error = document.querySelector('.error');
    let errMsgDes = "Description cannot be empty.";
    let errMsgType = "Please choose a type.";
    let errMsgMoney = "Currency cannot be empty.";
    evt.preventDefault();

    if (description == "" || description == null) {
        error.textContent = errMsgDes;
        evt.preventDefault();
        return false;
    } else if (option == "") {
        error.textContent = errMsgType;
        evt.preventDefault();
        return false;
    } else if (currency == "" || currency == null) {
        error.textContent = errMsgMoney;
        evt.preventDefault();
        return false;
    } else {
        error.textContent = "";
        addToTable(description, option, amount);
        evt.target.reset();
    }
    sumUpByType(option, currency);
})

// Function for creating table
function addToTable(description, option, amount) {
    // Create Row
    const row = tbody.insertRow(0);
    // Create Columns
    const tdDesciption = row.insertCell(0);
    const tdOption = row.insertCell(1);
    const tdCurrency = row.insertCell(2);
    const tdTool = row.insertCell(3);
    const tdDelete = document.createElement('i');
    // Set Attribute
    row.setAttribute('class', option);
    tdCurrency.setAttribute('class', 'amount');
    tdTool.setAttribute('class', 'tools');
    tdDelete.setAttribute('class', 'delete fa fa-trash-o');
    // Set Data
    tdDesciption.textContent = description;
    tdOption.textContent = option;
    tdCurrency.textContent = amount;
    // Build fragment
    tdTool.appendChild(tdDelete);
}


// Function for addition
function sumUpByType(option, currency) {
    const currencyInputs = document.querySelectorAll("input[name='currency']");
    for (let i = 0; i < currencyInputs.length; i++) {
        if (option == "debit") {
            debitTotal += Number(currency);
        }
        if (option == "credit") {
            creditTotal += Number(currency);
        }
    }
    debitDisplay.textContent = formatter.format(debitTotal);
    creditDisplay.textContent = formatter.format(creditTotal);
}

// Function for removing item and subduction
tbody.addEventListener('click', function (evt) {
    const tool = evt.target.parentNode;

    if (evt.target.classList.contains('delete')) {
        if (confirm('Are you sure you want to 【DELETE】 this record?')) {
            tbody.removeChild(tool.parentNode);

            if (tool.parentNode.classList.contains('debit')) {
                debitTotal -= Number((tool.previousSibling.textContent).replace(/[^0-9\.-]+/g, ""));
            }
            if (tool.parentNode.classList.contains('credit')) {
                creditTotal -= Number((tool.previousSibling.textContent).replace(/[^0-9\.-]+/g, ""));
            }
        }
        debitDisplay.textContent = formatter.format(debitTotal);
        creditDisplay.textContent = formatter.format(creditTotal);
    }
})

// Function for reloading
window.addEventListener('mouseover', () => {
    clearInterval(timer);
    timer = setInterval(() => {
        alert("Hello!\nThe mouse did not move in 2 mins.\nLet" + "'" + "s reload the page.");
        window.location.reload();
    }, 120000);
})