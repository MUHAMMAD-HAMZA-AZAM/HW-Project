
$(document).ready(function () {
  (function ($) {
    "use strict"; // Start of use strict
    //
    $('.carousel.carousel-multi-item.v-2 .carousel-item').each(function () {
      var next = $(this).next();
      if (!next.length) {
        next = $(this).siblings(':first');
      }
      next.children(':first-child').clone().appendTo($(this));

      for (var i = 0; i < 4; i++) {
        next = next.next();
        if (!next.length) {
          next = $(this).siblings(':first');
        }
        next.children(':first-child').clone().appendTo($(this));
      }
    });
    //
    // Smooth scrolling using jQuery easing
    $('a.js-scroll-trigger[href*="#"]:not([href="#"])').click(function () {
      if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
        var target = $(this.hash);
        target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
        if (target.length) {
          $('html, body').animate({
            scrollTop: (target.offset().top - 70)
          }, 1000, "easeInOutExpo");
          return false;
        }
      }
    });

    // Closes responsive menu when a scroll trigger link is clicked
    $('.js-scroll-trigger').click(function () {
      $('.navbar-collapse').collapse('hide');
    });
    // Activate scrollspy to add active class to navbar items on scroll
    //$('body').scrollspy({
    //  target: '#mainNav',
    //  offset: 100
    //});

    // Collapse Navbar
    var navbarCollapse = function () {
      var mainNav = $("#mainNav");
      if (mainNav.length) {
        if ($("#mainNav").offset().top > 100) {
          $("#mainNav").addClass("navbar-shrink");
          $("#mainNav").addClass("navWhiteColor").removeClass("navCustomColor");

        } else {
          $("#mainNav").removeClass("navbar-shrink");
          $("#mainNav").addClass("navCustomColor").removeClass("navWhiteColor");

        }
      }
    };
    // Collapse now if page is not at top
    navbarCollapse();
    // Collapse the navbar when page is scrolled
    $(window).scroll(navbarCollapse);

    $("#rightarr").click(function () {
      $("div").animate({
        marginLeft: (parseInt($("div").css("marginLeft")) + parseInt($("#marginTop").val())) + 'px'
      }, "slow");
    });
    $(document).on('click', '.nav-item', function () {
      $(".ms-tab-icon-img").removeClass('ms-active-tab');
      if ($(this).parents().hasClass('modal-popup-lp')) {
        $(this).find('.ms-tab-icon-img').addClass('ms-active-tab');
      }
    })

    //$(function () {
    //  console.log($("#nlp-sw"));
    //  $("#nlp-sw").owlCarousel({
    //    slideSpeed: 2000,
    //    paginationSpeed: 2000,
    //    singleItem: true,
    //    autoPlay: true,
    //    autoplaySpeed: 8000,
    //    loop: true,
    //  });
    //})
    //$(function () {
    //  $(".tm-slider-wrapper").owlCarousel({
    //    slideSpeed: 1000,
    //    paginationSpeed: 1000,
    //    singleItem: true,
    //    autoPlay: true,
    //    autoplaySpeed: 8000,
    //    loop: true,
    //  });
    //})

    $("body").on("click", ".fab-bg , #nd-left-menu > ul> li", function (event) {
      event.stopImmediatePropagation();
      $('#nd-ap').toggleClass('ap-left');
    });
    $("body").on("click", ".hwm_cs > i", function (event) {
      $(".hwm_cs").toggleClass('ml-0');
    });

  })(jQuery); // End of use strict
});



