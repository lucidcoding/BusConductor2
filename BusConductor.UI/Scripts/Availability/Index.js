BusConductor.Availability.Index = function () {

    var initialize = function () {

        $(".earlier, .later").click(function (event) {
            event.preventDefault();

            var url = $(this).attr("href");

            $.ajax({
                url: url,
                type: "GET",
                success: function (result) {
                    $("#AvailabilityIndexCalendarPlaceholder").replaceWith(result);
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
    BusConductor.Availability.Index.initialize();
});