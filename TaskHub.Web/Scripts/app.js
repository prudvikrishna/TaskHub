
$(document).ready(function () {


  //  if ($(".date-picker").length > 0) { $(".date-picker").datepicker(); }

$(".date-picker").on("change", function () {
    var id = $(this).attr("id");
    var val = $("label[for='" + id + "']").text();
    $("#msg").text(val + " changed");
});




/*
$('.header-nav').affix({
    offset: {
        top: 160
    }
});
$('#deleteModal').modal({
    show: false
});*/  
});