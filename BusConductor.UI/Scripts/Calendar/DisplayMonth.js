BusConductor.Calendar.DisplayMonth = function () {

    var initialize = function () {

        $(".previous-month, .next-month").click(function (event) {
            event.preventDefault();

            var url = $(this).attr("href");

            $.ajax({
                url: url,
                type: "GET",
                success: function (result) {
                    $("#CalendarPlaceholder").replaceWith(result);
                    initialize();
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    alert('error: ' + jqXhr + ', ' + textStatus + ', ' + errorThrown);
                }
            });
        });
    };
    
    return { initialize: initialize };
} ();



$(document).ready(function () {
    BusConductor.Calendar.DisplayMonth.initialize();
});