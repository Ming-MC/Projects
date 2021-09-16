// Enter JavaScript for the exercise here...
//! 01 Way to create table

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

// Validate input
form.addEventListener('submit', function (evt) {
    const description = evt.target.elements['description'].value;
    const option = evt.target.elements['type'].value;
    const currency = evt.target.elements['currency'].value;
    const amount = formatter.format(currency);
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

// Create Elements and add datas to columns
function addToTable(description, option, amount) {
    // Create Elements
    let tr = document.createElement('tr');
    let tdDescription = document.createElement('td');
    let tdOption = document.createElement('td');
    let tdCurrency = document.createElement('td');
    let tdTool = document.createElement('td');
    let tdDelete = document.createElement('i');

    // Create TextNode
    let txtDescription = document.createTextNode(description);
    let txtOption = document.createTextNode(option);
    let txtCurrency = document.createTextNode(amount);

    // Set Attribute
    tr.setAttribute('class', option);
    tdCurrency.setAttribute('class', 'amount');
    tdTool.setAttribute('class', 'tools');
    tdDelete.setAttribute('class', 'delete fa fa-trash-o');

    // build document fragment
    tdDescription.appendChild(txtDescription);
    tdOption.appendChild(txtOption);
    tdCurrency.appendChild(txtCurrency);
    tr.appendChild(tdDescription);
    tr.appendChild(tdOption);
    tr.appendChild(tdCurrency);
    tr.appendChild(tdTool);
    tdTool.appendChild(tdDelete)
    tbody.appendChild(tr);
}

// Sum up datas by type
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

// Delete Item
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

// Reload page
window.addEventListener('mouseover', () => {
    clearInterval(timer);
    timer = setInterval(() => {
        alert("Hello!\nThe mouse did not move in 2 mins.\nLet" + "'" + "s reload the page.");
        window.location.reload();
    }, 120000);
})