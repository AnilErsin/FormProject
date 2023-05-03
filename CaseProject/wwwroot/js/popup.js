
    $(document).ready(function () {
        $("#popup-button").click(function () {
            $("#popup").fadeIn();
        });
    $("#cancel-button").click(function () {
        $("#popup").fadeOut();
            });
        });

    $(document).on("click", "#submitForm", function () {
            var name = $("#name").val();
    var description = $("#description").val();
    var createdAt = $("#created-at").val();
    var createdBy = $("#created-by").val();

    var formData = {
        Name: name,
    Description: description,
    CreatedAt: createdAt,
    CreatedBy: createdBy
            };

    if (!name || !description || !createdAt || !createdBy) {
        alert("Lütfen tüm alanları doldurun.");
    return;
            }
    $.ajax({
        type: "POST",
    url: "/Home/CreateFrom",
    data: JSON.stringify(formData),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function () {

        location.reload();
    $("#popup").modal("hide");
                    
                   
                },
    error: function (xhr, status, error) {
        alert("Kaydetme işlemi sırasında bir hata oluştu: " + xhr.responseText);
                }
            });
        });
