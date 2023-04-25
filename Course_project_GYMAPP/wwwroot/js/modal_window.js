function showMessage(par) {
    const action = parameters.action;
    const modal_w = $('#modal');

    if (action === undefined) {
        alert('Помилка')
        return;
    }

    $.ajax({
        type: 'GET',
        url: action,
        success: function (response) {
            modal_w.find(".modal-body").html(response);
            modal_w.modal('show')
        },
        failure: function () {
            modal_w.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};