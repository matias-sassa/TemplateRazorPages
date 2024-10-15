// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ToastifyError(message) {
    Toastify({
        text: message,
        gravity: "bottom",
        style: {
            background: "linear-gradient(to right, #cb2d3e, #ef473a)",
        }
    }).showToast();
}


function ToastifySuccess(message) {
    Toastify({
        text: message,
        gravity: "bottom",
        style: {
            background: "linear-gradient(to right, #11998e, #38ef7d)",
        }
    }).showToast();
}