//----------------------------------------------------------------
// >>> TABLE OF CONTENTS:
//----------------------------------------------------------------

// 01. Mobile Menu
// 02. Header Dropdown Menu
// 03. Select List (Dropdown)
// 04. Facts Counter
// 05. Category Filter (MixItUp Plugin)
// 06. Vertical Tabs
// 07. Blog Tags (Tooltip)
// 08. Owl Carousel
// 09. Sidebar Accordion
// 10. Responsive Tabs
// 11. Responsive Table
// 12. Form Fields (Value Disappear on Focus)
// 13. Bootstrap Carousel Swipe (Testimonials Carousel)
// 14. Bx Carousel
// 15. Contact Form Submit/Validation
// 16. Masonry

//----------------------------------------------------------------

$(function () {
    'use strict';

    //Mobile Menu
    //--------------------------------------------------------
    var bodyObj = $('body');
    var MenuObj = $("#menu");
    var mobileMenuObj = $('#mobile-menu');

    bodyObj.wrapInner('<div id="wrap"></div>');

    var toggleMenu = {
        elem: MenuObj,
        mobile: function () {
            //activate mmenu
            mobileMenuObj.mmenu({
                slidingSubmenus: false,
                position: 'right',
                zposition: 'front'
            }, {
                pageSelector: '#wrap'
            });

            //hide desktop top menu
            this.elem.hide();
        },
        desktop: function () {
            //close the menu
            mobileMenuObj.trigger("close.mm");

            //reshow desktop menu
            this.elem.show();
        }
    };

    Harvey.attach('screen and (max-width:991px)', {
        setup: function () {
            //called when the query becomes valid for the first time
        },
        on: function () {
            //called each time the query is activated
            toggleMenu.mobile();
        },
        off: function () {
            //called each time the query is deactivated
        }
    });

    Harvey.attach('screen and (min-width:992px)', {
        setup: function () {
            //called when the query becomes valid for the first time
        },
        on: function () {
            //called each time the query is activated
            toggleMenu.desktop();
        },
        off: function () {
            //called each time the query is deactivated
        }
    });

    //Header Dropdown Menu
    //--------------------------------------------------------
    var megaMenuHasChildren = $('.dropdown');
    var megaMenuDropdownMenu = $('.dropdown-menu');

    megaMenuHasChildren.on({
        mouseenter: function () {
            if (navigator.userAgent.match(/iPad/i) !== null) {
                $(this).find(megaMenuDropdownMenu).stop(true, true).slideDown('400');
            } else {
                $(this).find(megaMenuDropdownMenu).stop(true, true).delay(400).slideDown();
            }
        }, mouseleave: function () {
            if (navigator.userAgent.match(/iPad/i) !== null) {
                $(this).find(megaMenuDropdownMenu).stop(true, true).slideUp('400');
            } else {
                $(this).find(megaMenuDropdownMenu).stop(true, true).delay(400).slideUp();
            }
        }
    });

    //Select List (Dropdown)
    //--------------------------------------------------------
    var selectObj = $('select');
    var selectListObj = $('ul.select-list');
    selectObj.each(function () {
        var $this = $(this), numberOfOptions = $(this).children('option').length;

        $this.addClass('select-hidden');
        $this.wrap('<div class="select"></div>');
        $this.after('<div class="select-styled"></div>');

        var $styledSelect = $this.next('div.select-styled');
        $styledSelect.text($this.children('option').eq(0).text());

        var $list = $('<ul />', {
            'class': 'select-list'
        }).insertAfter($styledSelect);

        for (var i = 0; i < numberOfOptions; i++) {
            $('<li />', {
                text: $this.children('option').eq(i).text(),
                rel: $this.children('option').eq(i).val()
            }).appendTo($list);
        }

        var $listItems = $list.children('li');

        $styledSelect.on('click', function (e) {
            e.stopPropagation();
            $('div.select-styled.active').not(this).each(function () {
                $(this).removeClass('active').next(selectListObj).hide();
            });
            $(this).toggleClass('active').next(selectListObj).toggle();
        });

        $listItems.on('click', function (e) {
            e.stopPropagation();
            $styledSelect.text($(this).text()).removeClass('active');
            $this.val($(this).attr('rel'));
            $list.hide();
        });

        $(document).on('click', function () {
            $styledSelect.removeClass('active');
            $list.hide();
        });

    });

    //Facts Counter
    //--------------------------------------------------------
    var counterObj = $('.fact-counter');
    counterObj.counterUp({
        delay: 10,
        time: 500
    });

    //Category Filter (MixItUp Plugin)
    //--------------------------------------------------------
    var folioFilterObj = $('#category-filter');
    folioFilterObj.mixItUp();

    //Vertical Tabs
    //--------------------------------------------------------
    var tabObject = $(".tabs-menu li");
    var tabContent = $(".tabs-list .tab-content");
    tabObject.on('click', function (e) {
        e.preventDefault();
        $(this).siblings('li.active').removeClass("active");
        $(this).addClass("active");
        var index = $(this).index();
        tabContent.removeClass("active");
        tabContent.eq(index).addClass("active");
    });

    //Blog Tags (Tooltip)
    //--------------------------------------------------------
    var tagObj = $('[data-toggle="blog-tags"]');
    tagObj.tooltip();

    //Owl Carousel
    //--------------------------------------------------------
    var owlObj = $('.owl-carousel');
    owlObj.owlCarousel({
        loop: false,
        margin: 30,
        nav: false,
        dots: true,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 2
            }
        }
    });

    var owlEventObj = $('.owl-carousel-event');
    owlEventObj.owlCarousel({
        loop: false,
        margin: 30,
        nav: false,
        dots: true,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            1200: {
                items: 3
            }
        }
    });

    //Sidebar Accordion
    //--------------------------------------------------------
    var secondaryObj = $('#secondary [data-accordion]');
    var multipleObj = $('#multiple [data-accordion]');
    var singleObj = $('#single[data-accordion]');

    secondaryObj.accordion({
        singleOpen: true
    });

    multipleObj.accordion({
        singleOpen: false
    });

    singleObj.accordion({
        transitionEasing: 'cubic-bezier(0.455, 0.030, 0.515, 0.955)',
        transitionSpeed: 200
    });

    //Responsive Tabs
    //--------------------------------------------------------
    var restabObj = $('#responsiveTabs');
    restabObj.responsiveTabs({
        startCollapsed: 'accordion'
    });

    //Responsive Tables
    //--------------------------------------------------------
    var tableObj = $('.table');
    var shoptableObj = $('.shop_table');
    tableObj.basictable({
        breakpoint: 991
    });

    shoptableObj.basictable({
        breakpoint: 991
    });

    //Form Fields (Value Disappear on Focus)
    //--------------------------------------------------------
    var requiredFieldObj = $('.input-required');

    requiredFieldObj.find('input').on('focus',function(){
        if(!$(this).parent(requiredFieldObj).find('label').hasClass('hide')){
            $(this).parent(requiredFieldObj).find('label').addClass('hide');
        }
    });
    requiredFieldObj.find('input').on('blur',function(){
        if($(this).val() === '' && $(this).parent(requiredFieldObj).find('label').hasClass('hide')){
            $(this).parent(requiredFieldObj).find('label').removeClass('hide');
        }
    });

    //Bootstrap Carousel Swipe (Testimonials Carousel)
    //--------------------------------------------------------
    var testimonialsObj = $("#testimonials");
    testimonialsObj.swiperight(function () {
        $(this).carousel('prev');
    });
    testimonialsObj.swipeleft(function () {
        $(this).carousel('next');
    });

    //Bx Carousel
    //--------------------------------------------------------

    //Popular Items Detail V1

    var popularSlidesD1 = 2;
    var popularWidthD1 = 370;
    var popularMarginD1 = 54;

    if($(window).width() <= 1199) {
        popularSlidesD1 = 2;
        popularWidthD1 = 330;
        popularMarginD1 = 37;
    }
    if($(window).width() <= 991) {
        popularSlidesD1 = 2;
        popularWidthD1 = 350;
        popularMarginD1 = 20;
    }
    if($(window).width() <= 767) {
        popularSlidesD1 = 1;
        popularWidthD1 = 320;
        popularMarginD1 = 0;
    }

    var popularItemObjD1 = $('.popular-items-detail-v1');
    popularItemObjD1.bxSlider({
        minSlides: 1,
        maxSlides: popularSlidesD1,
        slideWidth: popularWidthD1,
        slideMargin: popularMarginD1,
        responsive: true,
        touchEnabled: true,
        controls: false,
        infiniteLoop: true,
        shrinkItems: true
    });

    //Popular Items Detail V2

    var popularSlidesD2 = 3;
    var popularWidthD2 = 360;
    var popularMarginD2 = 30;

    if($(window).width() <= 1199) {
        popularSlidesD2 = 3;
        popularWidthD2 = 300;
        popularMarginD2 = 20;
    }
    if($(window).width() <= 991) {
        popularSlidesD2 = 2;
        popularWidthD2 = 350;
        popularMarginD2 = 20;
    }
    if($(window).width() <= 767) {
        popularSlidesD2 = 1;
        popularWidthD2 = 320;
        popularMarginD2 = 0;
    }

    var popularItemObjD2 = $('.popular-items-detail-v2');
    popularItemObjD2.bxSlider({
        minSlides: 1,
        maxSlides: popularSlidesD2,
        slideWidth: popularWidthD2,
        slideMargin: popularMarginD2,
        responsive: true,
        touchEnabled: true,
        controls: false,
        infiniteLoop: true,
        shrinkItems: true
    });

    //Contact Form Submit/Validation
    //--------------------------------------------------------
    var emailerrorvalidation = 0;
    var formObj = $('#contact');
    var contactFormObj = $('#submit-contact-form');
    var firstNameFieldObj = $("#first-name");
    var lastNameFieldObj = $("#last-name");
    var emailFieldObj = $("#email");
    var phoneFieldObj = $("#phone");
    var messageFieldObj = $("#message");
    var successObj = $('#success');
    var errorObj = $('#error');

    contactFormObj.on('click', function () {
        var emailaddress = emailFieldObj.val();
        function validateEmail(emailaddress) {
            var filter = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
            if (filter.test(emailaddress)) {
                return true;
            } else {
                return false;
            }
        }

        var data = {
            firstname: firstNameFieldObj.val(),
            lastname: lastNameFieldObj.val(),
            email: emailFieldObj.val(),
            phone: phoneFieldObj.val(),
            message: messageFieldObj.val()
        };
        if (data.firstname === '' || data.lastname === '' || data.email === '' || data.phone === '' || data.message === '') {
            alert("All fields are mandatory");
        } else {
            if (validateEmail(emailaddress)) {
                if (emailerrorvalidation === 1) {
                    alert('Nice! your Email is valid, you can proceed now.');
                }
                emailerrorvalidation = 0;
                $.ajax({
                    type: "POST",
                    url: "contact.php",
                    data: data,
                    cache: false,
                    success: function () {
                        successObj.fadeIn(1000);
                        formObj[0].reset();
                    },
                    error: function () {
                        errorObj.fadeIn(1000);
                    }
                });
            } else {
                emailerrorvalidation = 1;
                alert('Oops! Invalid Email Address');
            }
        }
        return false;
    });

    var currentUser = null;
    function user(username) {
        currentUser = username;
    }

    //Login Form Submit/Validation
    //--------------------------------------------------------
    $('#submit-loginForm').on('click', function () {
        var loginData = {
            grant_type: 'password',
            username: $('#email-signin').val(),
            password: $('#password-signin').val()
        };
        $.ajax({
            type: "POST",
            url: "http://localhost:64110/Token",
            data: loginData,
            success: function (data) {
                //self.user(data.userName);
                sessionStorage.setItem('tokenKey', data.access_token);
                window.location.replace("http://localhost:50164");
            },
            error: function () {
                alert("Something went wrong. " + xhr.responseText);
            }
        });
    });

    //Register Form Submit/Validation
    //--------------------------------------------------------
    $('#submit-registerForm').on('click', function () {
        var data = {
            Email: $('#email-signup').val(),
            Password: $('#password-signup').val(),
            ConfirmPassword: $('#confirm-password-signup').val(),
            UserRole: $('#user-role').val()
        };
        $.ajax({
            type: "POST",
            url: 'http://localhost:64110/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (data) {
                //self.user(data.userName);
                alert("Registration Successful! Please use the signin form to login.")
            },
            error: function (xhr) {
                alert("Something went wrong. " + xhr.responseText);
            }
        });
    });

    //User State
    //--------------------------------------------------------
    self.HasToken = function () {
        if (sessionStorage.getItem('tokenKey')) {
            return true;
        }
        return false;
    }

    //Change Login Button
    //--------------------------------------------------------
    if (self.HasToken()) {
        var logoutIconEl = '<i class="fa fa-sign-out"></i>';
        $('#loginBtns').text(' Logout').prepend(logoutIconEl);
        $('#loginBtns').attr('href', 'Logout');
    }

    //Logout
    //--------------------------------------------------------
    if ($('#loginBtns').text() == " Logout") {
        $('#loginBtns').on('click', function (e) {
            e.preventDefault();
            sessionStorage.removeItem('tokenKey');
            window.location.replace("http://localhost:50164");
        });
    }

    //Logout
    //--------------------------------------------------------
    //self.GetBooks = function () {
    //    $.ajax({
    //        type: "GET",
    //        url: "http://localhost:64110/api/books",
    //        success: function (data) {
    //            $.each(data, function (i) {
    //                var bookEl = '<article>' +
    //                    '<div class="single-book-box"> ' +
    //                    '<div class="post-thumbnail">' +
    //                    '<div class="book-list-icon yellow-icon"></div>' +
    //                    '<a href=""><img alt="Book" src="~/images/books-media/list-view/book-media-01.jpg" /></a>' +
    //                    '</div>' +
    //                    '<div class="post-detail">' +
    //                    '<div class="books-social-sharing">' +
    //                    '<ul>' +
    //                    '<li><a href="#" target="_blank"><i class="fa fa-facebook"></i></a></li>' +
    //                    '<li><a href="#" target="_blank"><i class="fa fa-twitter"></i></a></li>' +
    //                    '<li><a href="#" target="_blank"><i class="fa fa-google-plus"></i></a></li>' +
    //                    '<li><a href="#" target="_blank"><i class="fa fa-rss"></i></a></li>' +
    //                    '<li><a href="#" target="_blank"><i class="fa fa-linkedin"></i></a></li>' +
    //                    '</ul>' +
    //                    '</div>' +
    //                    '<div class="optional-links">' +
    //                    '<ul>' +
    //                    '<li>' +
    //                    '<a href="#" target="_blank" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
    //                    '<i class="fa fa-shopping-cart"></i>' +
    //                    '</a>' +
    //                    '</li>' +
    //                    '<li>' +
    //                    '<a href="#" target="_blank" data-toggle="blog-tags" data-placement="top" title="Like">' +
    //                    '<i class="fa fa-heart"></i>' +
    //                    '</a>' +
    //                    '</li>' +
    //                    '<li>' +
    //                    '<a href="#" target="_blank" data-toggle="blog-tags" data-placement="top" title="Mail">' +
    //                    '<i class="fa fa-envelope"></i>' +
    //                    '</a>' +
    //                    '</li>' +
    //                    '<li>' +
    //                    '<a href="#" target="_blank" data-toggle="blog-tags" data-placement="top" title="Search">' +
    //                    '<i class="fa fa-search"></i>' +
    //                    '</a>' +
    //                    '</li>' +
    //                    '<li>' +
    //                    '<a href="#" target="_blank" data-toggle="blog-tags" data-placement="top" title="Print">' +
    //                    '<i class="fa fa-print"></i>' +
    //                    '</a>' +
    //                    '</li>' +
    //                    '</ul>' +
    //                    '</div>' +
    //                    '<header class="entry-header">' +
    //                    '<div class="row">' +
    //                    '<div class="col-sm-6">' +
    //                    '<h3 class="entry-title">' +
    //                    '<a href="books-media-detail-v1.html">The Great Gatsby</a>' +
    //                    '</h3>' +
    //                    '<ul>' +
    //                    '<li><strong>Author:</strong> F. Scott Fitzgerald</li>' +
    //                    '<li><strong>ISBN:</strong> 9781581573268</li>' +
    //                    '</ul>' +
    //                    '</div>' +
    //                    '<div class="col-sm-6">' +
    //                    '<ul>' +
    //                    '<li><strong>Edition:</strong> First editio</li>' +
    //                    '<li><strong>Local Availability:</strong> 0 (of 1)</li>' +
    //                    '<li>' +
    //                    '<div class="rating">' +
    //                    '<strong>Rating: </strong>' +
    //                    '<span></span>' +
    //                    '<span></span>' +
    //                    '<span></span>' +
    //                    '<span></span>' +
    //                    '<span></span>' +
    //                    '</div>' +
    //                    '</li>' +
    //                    '</ul>' +
    //                    '</div>' +
    //                    '</div>' +
    //                    '</header>' +
    //                    '<div class="entry-content">' +
    //                    '<p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which dont look even slightly believable.</p>' +
    //                    '</div>' +
    //                    '<footer class="entry-footer">' +
    //                    '<a class="btn btn-dark-gray" href="books-media-detail-v1.html">Read More</a>' +
    //                    '</footer>' +
    //                    '</div>' +
    //                    '<div class="clear"></div>' +
    //                    '</div >' +
    //                    '</article >';
    //                $('.books-list').append(bookEl);
    //            });
    //        },
    //        error: function () {
    //            alert("Something went wrong.");
    //        }
    //    });
    //}
});





$( window ).load(function() {
    //Masonry
    //--------------------------------------------------------
    var girdFieldObj = $('.grid');
    girdFieldObj.masonry({
        itemSelector: '.grid-item',
        percentPosition: true
    });
});