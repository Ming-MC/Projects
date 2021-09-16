//! 02 Way to create table
//! --> not complete

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
    const datas = [{
        description: description,
        option: option,
        currency: amount
    }]

    for (let i = 0; i < datas.length; i++) {

        let tr = document.createElement('tr');
        tr.setAttribute('class', option)
        tbody.appendChild(tr);

        for (let k in datas[i]) {

            let td = document.createElement('td');
            td.textContent = datas[i][k];
            tr.appendChild(td);
        }

        let tdTool = document.createElement('td');
        tdTool.setAttribute('class', 'tools');
        let toolDelete = document.createElement('i');
        toolDelete.setAttribute('class', 'delete fa fa-trash-o');
        tr.appendChild(tdTool);
        tdTool.appendChild(toolDelete);
    }

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

    // debitDisplay.textContent = `$${debitTotal.toFixed(2)}`;
    // creditDisplay.textContent = `$${creditTotal.toFixed(2)}`;
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