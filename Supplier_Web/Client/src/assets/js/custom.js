$(document).ready(function () {
  $(document).on("click", ".tab-search-icon", function (e) {
    $('.sm_top_search').removeClass('d-none');
  })
  $(document).on("click", ".sm_top_search i", function (e) {
    $('.sm_top_search').addClass('d-none');
  })
  $(document).on("click", ".collpase-btn button", function (e) {
    const icon = $(this).find("i");
    icon.hasClass('fa-minus-circle') ? icon.removeClass('fa-minus-circle').addClass('fa-plus-circle') : icon.removeClass('fa-plus-circle').addClass('fa-minus-circle');
  })
})




  //const mainNavHeading = document.querySelector('.main-nav-heading');
  //debugger;
  //const tabSearchIcon = document.querySelector('.tab-search-icon');
  //const tabSerchOverlay = document.querySelector('.sm_top_search');
  //const timesIcon = document.querySelector('.sm_top_search i');
  //tabSearchIcon.addEventListener('click', event => {
  //  tabSerchOverlay.classList.remove('d-none');
  //})
  //timesIcon.addEventListener('click', event => {
  //  tabSerchOverlay.classList.add('d-none');
  //})

  //const mainCategories = document.querySelectorAll(".main_categories li")
  //const subCategories = document.querySelector(".sub_categories")
  //const subCategoriesLi = document.querySelectorAll(".sub_categories li")
  //const categoryGroup = document.querySelector(".category_group")
  //mainCategories.forEach(subLi => {
  //  subLi.addEventListener('click', event => {
  //    subCategories.classList.remove('d-none');
  //    subCategories.classList.add('d-block');
  //  })
  //})
  //subCategoriesLi.forEach(subGroupLi => {
  //  subGroupLi.addEventListener('click', event => {
  //    categoryGroup.classList.remove('d-none');
  //    categoryGroup.classList.add('d-block');
  //  })
  //})
  //mainNavHeading.addEventListener("mouseleave", mouseOut => {
  //  subCategories.classList.add('d-none');
  //  subCategories.classList.remove('d-block');
  //  categoryGroup.classList.add('d-none');
  //  categoryGroup.classList.remove('d-block');
  //})

