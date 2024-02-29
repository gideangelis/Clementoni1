function caricamentoListPersona() {
    $.get({
        url: "/Home/_ListPersona",
        cache: false
    }).done(function (data) {
        $("#divListPersona").html(data);
    }).fail(function (data) {
        console.log("Errore List");
    });

}


function caricamentoFormPersona() {
    $.get({
        url: "/Home/_FormPersona",
        cache: false
    }).done(function (data) {
        $("#divFormPersona").html(data);
    }).fail(function (data) {
        console.log("Errore Form")
    })
}

function modificaPersona(id) {
    $.ajax({
        type: "POST",
        url: "/Home/_EditPersona",
        data: { id: id },

        success: function (data) {
            $("#divPopupPersona").html(data);
        }
    });
}