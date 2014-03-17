TaskIndex = function () {

    var initialise = function () {

        $('form').submit(function () {
            //if ($(this).valid()) {
            $.ajax({
                url: "Task/Index",
                type: "POST",
                data: $(this).serialize(),
                success: function (result) {
                    $("#TaskList").html(result);
                    TaskIndexTask.initialise();
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    alert('error: ' + jqXhr + ', ' + textStatus + ', ' + errorThrown);
                }
            });
            //}
            return false;
        });

        TaskIndexTask.initialise();
    };

    return { initialise: initialise };
} ();

$(document).ready(function () {
    TaskIndex.initialise();
});