TaskIndexTask = function () {

    var initialise = function () {
        $(".cancel-link").click(function (e) {
            e.preventDefault();

            var taskId = $(this).attr('task-id');
            var formData = $('form').serialize();

            $.ajax({
                url: "Task/Cancel",
                type: "POST",
                data: formData + "&taskId=" + taskId,
                success: function (result) {
                    $("#TaskList").html(result);
                    TaskIndexTask.initialise();
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    alert('error: ' + jqXhr + ', ' + textStatus + ', ' + errorThrown);
                }
            });
        });
    };

    return { initialise: initialise };
} ();