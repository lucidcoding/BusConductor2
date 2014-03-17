BusConductor.Booking.Make = function () {

    var initialize = function () {

        var datepickerParameters = {
            firstDay: 1,
            dateFormat: 'dd/mm/yy',
            showOn: "button",
            defaultDate: new Date(),
            beforeShowDay: function (date) {
                var day = date.getDay();

                if (day == 1 || day == 5) {
                    return [true];
                } else {
                    return [false];
                }
            },
            buttonImage: "/Content/Images/CalendarIcon.png"
            /*,
            buttonImageOnly: true*/
        };

        $("#PickUp").datepicker(datepickerParameters);
        $("#DropOff").datepicker(datepickerParameters);

        $("#IsMainDriver").change(function (event) {
            if (this.checked == true) {
                $(".alternate-driver").addClass("hide");
            } else {
                $(".alternate-driver").removeClass("hide");
            }
        });
    };

    return { initialize: initialize };
} ();


$(document).ready(function () {
    BusConductor.Booking.Make.initialize();
});