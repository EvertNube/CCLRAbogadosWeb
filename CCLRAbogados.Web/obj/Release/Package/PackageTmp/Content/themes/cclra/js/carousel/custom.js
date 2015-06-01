(function($) {
 "use strict";

$(window).load(function(){
  $('.flexslider').flexslider({
    animation: "slide",
    animationLoop: false,
    itemWidth: 293,
    itemMargin: 0,
    pausePlay: true,
    start: function(slider){
      $('body').removeClass('loading');
    }
  });
});

$(window).load(function(){
  $('.flexslider2').flexslider({
    animation: "slide",
    animationLoop: false,
    itemWidth: 720,
    itemMargin: 0,
    pausePlay: true,
    start: function(slider){
      $('body').removeClass('loading');
    }
  });
});

})(jQuery);
