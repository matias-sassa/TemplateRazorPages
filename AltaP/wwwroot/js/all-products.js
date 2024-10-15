$(document).ready(function () {

    let table = new DataTable('#allProducts', {
        columnDefs: [
            {
                target: 0,
                visible: false,
                searchable: false
            },
            {
                orderable: false,
                targets: "_all"
            },
        ]
    });

    table.on('click', 'td.active-check', function () {
        let cell = table.cell(this);
        let checkbox = $(this).find('input[type="checkbox"]')
        let isActive = checkbox.prop('checked');
        
        let rowData = table.row(this).data();
        let id = rowData[0]; 

        //console.log($(table.cell(this).node()).find('input[type=checkbox]').prop('checked'));
        //console.log($(this).find('input[type="checkbox"]').prop('checked'))

        $.ajax({
            type: "POST",
            url: "/Products/AllProducts?handler=Activation",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: id, isActive: isActive }),
            dataType: "json",
            success: function (message) {
                ToastifySuccess(message);
            },
            error: function (xhr, status, error) {
                ToastifyError(xhr.responseText);
                table.row(this).data(rowData).draw();
            }
        });
    });

    table.on('click', 'button.delete-product', function () {
        let tr = $(this).closest('tr');
        let rowData = table.row(tr).data();

        let id = rowData[0];
        let name = rowData[1];

        Swal.fire({
            title: '¿Estás seguro?',
            text: `Eliminarás el producto ${name}`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: "/Products/AllProducts?handler=Delete",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id: id }),
                    dataType: "json",
                    success: function (message) {
                        ToastifySuccess(message);
                        table.row(tr).remove().draw();
                    },
                    error: function (xhr) {
                        ToastifyError(xhr.responseText);
                    }
                });
            }
        });
    });

});

