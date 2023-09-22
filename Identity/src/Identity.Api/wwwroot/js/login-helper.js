document.getElementById('buttonEnterAsManager').addEventListener('click', function (event) {
    onClickEnterAsManager();
});

document.getElementById('buttonEnterAsStaff').addEventListener('click', function (event) {
    onClickEnterAsStaff();
});

document.getElementById('buttonEnterAsCustomer').addEventListener('click', function (event) {
    onClickEnterAsCustomer();
});

function onClickEnterAsManager() {
    document.getElementById('inputUsername').value = "francisco";
    document.getElementById('inputPassword').value = "P4$$w0rd";

    setTimeout(() => {
        document.getElementById('buttonLogin').click();
    }, 1000);
}

function onClickEnterAsStaff() {
    document.getElementById('inputUsername').value = "ramon";
    document.getElementById('inputPassword').value = "P4$$w0rd";

    setTimeout(() => {
        document.getElementById('buttonLogin').click();
    }, 1000);
}

function onClickEnterAsCustomer() {
    document.getElementById('inputUsername').value = "chris";
    document.getElementById('inputPassword').value = "P4$$w0rd";

    setTimeout(() => {
        document.getElementById('buttonLogin').click();
    }, 1000);
}