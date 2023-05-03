
    (function ($) {
        function addFieldRow() {
            const fieldRow = $('<div class="field-row form-group"></div>');
            const checkbox = $('<div class="form-check"><input type="checkbox" class="form-check-input required"/><label class="form-check-label">Required</label></div>');
            const fieldNameInput = $('<input type="text" class="form-control field-name" placeholder="Field Name"/>');
            const selectBox = $('<select class="form-select field-data-type"><option value="">--Select a data type--</option><option value="date">date</option><option value="number">number</option><option value="text">text</option></select>');
            const selectedDataType = $('<p id="selected-data-type" class="mt-2"></p>');
            const removeButton = $('<button type="button" class="btn btn-danger remove-field">Remove</button>');

            fieldRow.append(checkbox);
            fieldRow.append(fieldNameInput);
            fieldRow.append(selectBox);
            fieldRow.append(selectedDataType);
            fieldRow.append(removeButton);

            removeButton.on('click', function () {
                fieldRow.remove();
            });

            $('#fields-container').append(fieldRow);
        }


        $('#add-field').on('click', function () {
            addFieldRow();
        });

    $('#add-fields-form').on('submit', function (event) {
        event.preventDefault();
        const formID = $('#add-field').val();
    const fields = [];
    $('.field-row').each(function () {
                const required = $(this).find('.required').prop('checked');
    const name = $(this).find('.field-name').val();
    const dataType = $(this).find('.field-data-type').val();
    fields.push({Required: required, Name: name, dataType: dataType, FormID: formID });
            });

    $.ajax({
        type: 'POST',
    url: '/Home/AddFields',
    data: JSON.stringify(fields),
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',
    success: function () {
        alert('Fields have been added successfully.');
    window.location.href = '/Home/Index';
                },
    error: function (xhr, status, error) {
        alert('An error occurred while adding fields: ' + xhr.responseText);
                }
            });
        });
    })(jQuery);

