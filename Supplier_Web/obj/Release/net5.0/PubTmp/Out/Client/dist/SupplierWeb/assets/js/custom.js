$(document).ready(function () {
  var window_width = jQuery(window).width();
  $("body").on("click", ".fab-bg , #nd-left-menu > ul> li", function (event) {
    event.stopImmediatePropagation();
    $('#nd-ap').toggleClass('ap-left');
  });
  $("#toggle-sidebar").on('click', function () {
    $(".marketplace-sidebar").slideToggle('slow');
  });
  $("body").on("click", ".category-menuu", function (event) {
    event.stopImmediatePropagation();
    $('#nd-ap').toggleClass('ap-left');
  });
})

