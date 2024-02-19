(function ($) {
    "use strict";

    jQuery(document).ready(function ($) {
       
        $(".slider-area").on("translate.owl.carousel", function () {
            $(".slider-content h1, .slider-content p").removeClass("animated fadeInUp").css({
                'opacity': '0'
            }

            );

            $(".slider-content a").removeClass("animated fadeInDown").css({
                'opacity': '0'
            }

            );
        }

        );

        $(".slider-area").on("translated.owl.carousel", function () {
            $(".slider-content h1, .slider-content p").addClass("animated fadeInUp").css({
                'opacity': '0'
            }

            );

            $(".slider-content a").addClass("animated fadeInDown").css({
                'opacity': '0'
            }

            );
        }

        );
        $('#loading').fadeOut();

    }

    );
}

(jQuery));