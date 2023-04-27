$(document).ready(function () {
    var popup = $("#popup");
    var form = $("#form");
    var table = $("table tbody");

    $("#add-button").click(function () {
        popup.show();
    });

    $("#close-button").click(function () {
        popup.hide();
    });

    $("#save-button").click(function () {
        // Form verilerini alın
        var name = $("#name").val();
        var description = $("#description").val();
        var createdDate = $("#created-date").val();

        // Tabloya yeni satır ekle
        var newRowContent =
            "<tr><td>" +
            name +
            "</td><td>" +
            description +
            "</td><td>" +
            createdDate +
            "</td></tr>";
        table.append(newRowContent);

        // Formu sıfırla ve popup'ı gizle
        form.trigger("reset");
        popup.hide();
    });
});

function sendMethodParam(obj) {
    $.ajax({
        url: "/Form/CreateForm",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (data) {
            console.log("Create success");
        }
    })
}