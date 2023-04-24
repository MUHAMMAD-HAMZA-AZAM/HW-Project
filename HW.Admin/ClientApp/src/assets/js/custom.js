//export function myTest() {
//  alert();
//}
//export function onchangeEvent() {
//  $(".onchangeEvent").on('click', function () {
//    alert();
//  })
//}
$(document).ready(function () {
  var imgId;
  var crtId;
  var elementStyle;
  var getSelectedText;
  $("#formgroup,#editFormGroup").on("change", "input:file", function (e) {
    imgId = "#img"+e.target.id;
    readURL(this);
    $(function () {
      $(".resize").resizable({
        handles: "ne, nw, se, sw",
        aspectRatio: true
      });
      $(".ui-resizable-handle-se").removeClass("ui-icon ui-icon-gripsmall-diagonal-se");
      //$(".drag").draggable();
    });
  })
  function readURL(input) {
    if (input.files && input.files[0]) {
      var reader = new FileReader();

      reader.onload = function (e) {
        $(imgId).removeClass("invisible");
        $(imgId).find("img").attr('src', e.target.result)
      }
      reader.readAsDataURL(input.files[0]);
    }
  }
  //$("#formgroup").on("focus", "input:text,textarea", function (e) {
  //  var elementType = $(this).prev().prop('nodeName');
  //  crtId = "#" + e.target.id;
  //  var elType = $(crtId).get(0).tagName;
  //  if (elType.toLowerCase() == "textarea") {
  //    if (!$(crtId).children('p').length > 0) {
  //      $(crtId).append("<p>Asif</p>")
  //    }
  //  }
  //  var el = $(this).parents(".elementHtml");
  //  if (el.length > 0) {
  //    $("#eleStyle").removeClass("invisible");
  //    var st = $("#eleStyle");
  //    el.prepend(st);
  //  }
  //});
  //$("#eleStyle").on('click', 'button', function (e) {
  //  
  //  elementStyle = $(this).val();
  //})

  $('#formgroup').on("select", crtId, function () {
    getSelectedText = getSelectedText;
    //alert(getSelectedText());
  });
  $("#generateTextArea").on('click', function (e) {
    var elementNameAttr = "crt"+ $(this).attr('name');
    CKEDITOR.replace(elementNameAttr);
  })
  $("#genrateImageLeftTextRight").on('click', function (e) {
    var elementNameAttr = "crt"+ $(this).attr('name');
    CKEDITOR.replace(elementNameAttr);
  })
  $("#genrateImageRightTextLeft").on('click', function (e) {
    var elementNameAttr = "crt" + $(this).attr('name');
    CKEDITOR.replace(elementNameAttr);
  });
  $("#submitData").on('click', function (e) {
    
    var genrateHtml = "";
    $(".getControlData").each(function (index, element) {
      
      var tagName = this.tagName.toLowerCase();
      var divId = this.id;
      //if (tagName == "input") {
      //  var titleText = $(this).val();
      //  if (titleText != "" | titleText != null) {
      //    var createElement = $("<p></p>");
      //    createElement.append(titleText);
      //    createElement.attr("style", "font-size: 36px;text-transform: capitalize;font-weight: bold;line-height: 1.3;");
      //    genrateHtml = '<div class="title-contents editPostContents">' + createElement.prop('outerHTML')+'</div>'; 
      //  }
      //}
      if (tagName == "textarea") {
        var attrName = $(this).attr('name');
        var ckEditorHtml ='<div class="editPostContents ta-contents">'+ CKEDITOR.instances[attrName].getData()+'</div>';
        genrateHtml += ckEditorHtml;
      }
      else if (tagName == "img") {
        let getImgSrc = $(this).attr('src');
        if (getImgSrc != "" | getImgSrc != null) {
          var createImgElement = '<img src="' + getImgSrc + '" style="width:100%;height:700px;object-fit:cover;padding-bottom:25px;" />';
          //createImgElement.src = getImgSrc;
          //var imageEl = $(this).prop('outerHTML');
          genrateHtml += '<div class="img-contents editPostContents">' + createImgElement +' </div>';
        }
      }
      if (divId == "imgLeft" | divId =="imgRight") {
        var img = $(this).find("img").prop('outerHTML');
        var textAreaNameAttr = $(this).find("textarea").attr('name');
        var ckEditorHtml = CKEDITOR.instances[textAreaNameAttr].getData();
        if (divId == "imgLeft") {
          var imageLeftTextRight = `
            <div class="row iltr-contents editPostContents imgAndTextWrapper" id="imgAndTextWrapper" style="margin-top:25px;" >
              <div class="col-xl-4">
                <div class="imgContainer">
                  `+ img + `
                </div>
              </div>
              <div class="col-xl-8">
                <div class="textContainer">
                  `+ ckEditorHtml + `
                </div>
              </div>
            </div>
            `
            genrateHtml += imageLeftTextRight;
        }
        else if (divId == "imgRight") {
          var imageLeftTextRight = `
          <div class="row irtl-contents editPostContents imgAndTextWrapper" id="imgAndTextWrapper" style="margin-top:25px;" >
            <div class="col-xl-8">
              <div class="textContainer">
                `+ ckEditorHtml + `
              </div>
            </div>
            <div class="col-xl-4">
              <div class="imgContainer">
                `+ img + `
              </div>
            </div>
          </div>
          `
          genrateHtml += imageLeftTextRight;
        }
      }
    });
    
    $("#showData").empty().append(genrateHtml);
  });


// ************************************************** EDIT POST  **************************************************************** //
// ************************************************** EDIT POST  **************************************************************** //
  //$("#bind-data").on('click', function () {
  if ($('#editPostData').length > 0) {
      let currentIndex;
      let isImageElement = false;
      let eImgSrc;
      let tName;
      let editedHtml = "";
      $('.editPostContents').each(function (index, element) {
        if ($(this).hasClass("ta-contents")) {
          let textAreaInput = `
            <div class="removeCreatedElement">
              <div class="row">
                <i class="fas fa-times clsRemoveElement"></i>
                <div class="col-xl-12">
                  <div class="form-group">
                    <label class='control-lable-name'>Textarea</label>
                  </div>
                </div>
              </div>
              <div class="row mtb15">
                <div class="col-xl-12 elementHtml" >
                    <textarea rows="3" name="ecrt` + index + `" placeholder="Start writing or typing" class="form-control getControlData"></textarea>
                </div>
              </div>
            </div>`
          $("#editFormGroup").append(textAreaInput);
          editedHtml += textAreaInput
          CKEDITOR.replace("ecrt" + index);
          tName = "ecrt" + index;
          CKEDITOR.instances[tName].setData($(this).html());
        }
        else if ($(this).hasClass("img-contents")) {
          currentIndex = index;
          isImageElement = true;
          eImgSrc = $(this).find('img').attr('src');
          let editImage = `
            <div class="removeCreatedElement">
              <div class="row">
                <i class="fas fa-times clsRemoveElement"></i>
                <div class="col-xl-12">
                  <div class="form-group">
                    <label class='control-lable-name'>Image</label>
                  </div>
                </div>
              </div>
              <div class="row">
                <div class="col-xl-12 elementHtml">
                  <div class="form-group onchangeEvent">
                    <label for="ecrtFile`+ index + `"><i class = "fas fa-upload"></i> Upload Image</label>
                    <input type="file" id="ecrtFile` + index + `" (change)="handleFileChange($event)" class="form-control" />
                    <div class="drag resize edit-image ui-widget-content" id="eimgecrtFile`+ index + `" >
                      <img src="`+ eImgSrc + `" class="getControlData" style="width:100%;height:700px;object-fit:cover;padding-bottom:25px;" alt="Uplaoded Image"/>
                    </div>
                  </div>
                </div>
              </div>
            </div>`
          $("#editFormGroup").append(editImage);
          editedHtml += editImage
        }
        else if ($(this).hasClass("iltr-contents")) {         
          currentIndex = index;
          isImageElement = true;
          eImgSrc = $(this).find('img').attr('src');
          let imageLeft = `
          <div class="imageTextPosition getControlData mtb15 removeCreatedElement" id="imgLeft">
            <i class="fas fa-times clsRemoveElement"></i>
            <div class="row">
              <div class="col-xl-12">
                <div class="form-group">
                  <label class='control-lable-name'>Image Left text right</label>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-xl-4"> 
                <div class="imagePosition onchangeEvent">
                  <label for="ecrtFile`+ index + `"><i class = "fas fa-upload"></i> Upload Image</label>
                  <input type="file" id="ecrtFile` + index + `" (change)="handleFileChange($event)" class="form-control" />
                  <div class="imageControl" id="eimgecrtFile`+ index + `" >
                    <img src="`+ eImgSrc + `" alt="Uplaoded Image"/>
                  </div>
                </div>
              </div>
              <div class="col-xl-8">
                <div class="textPosition">
                  <textarea rows="3" name="ecrt` + index + `" placeholder="Start writing or typing" class="form-control"></textarea>
                </div>
              </div>
            </div>
          </div>`
          $("#editFormGroup").append(imageLeft);
          editedHtml += imageLeft

          CKEDITOR.replace("ecrt" + index);
          tName = "ecrt" + index;
          CKEDITOR.instances[tName].setData($(this).find('.textContainer').html());
        }
        else if ($(this).hasClass("irtl-contents")) {
          currentIndex = index;
          isImageElement = true;
          eImgSrc = $(this).find('img').attr('src');
          let imageRight = `
          <div class="imageTextPosition getControlData mtb15 removeCreatedElement" id="imgRight">
            <i class="fas fa-times clsRemoveElement"></i>
            <div class="row">
              <div class="col-xl-12">
                <div class="form-group">
                  <label class='control-lable-name'>Image right text left</label>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-xl-8">
                <div class="textPosition">
                  <textarea rows="3" name="ecrt` + index + `" placeholder="Start writing or typing" class="form-control"></textarea>
                </div>
              </div>
              <div class="col-xl-4">
                <div class="imagePosition onchangeEvent">
                  <label for="ecrtFile`+ index + `"><i class = "fas fa-upload"></i> Upload Image</label>
                  <input type="file" id="ecrtFile` + index + `" (change)="handleFileChange($event)" class="form-control" />
                  <div class="imageControl" id="eimgecrtFile`+ index + `" >
                    <img src="`+ eImgSrc + `" alt="Uplaoded Image"/>
                  </div>
                </div>
              </div>
            </div>
          </div>`
          $("#editFormGroup").append(imageRight);
          editedHtml += imageRight;

          CKEDITOR.replace("ecrt" + index);
          tName = "ecrt" + index;
          CKEDITOR.instances[tName].setData($(this).find('.textContainer').html());

        }

      });
    }
// ************************************************** Remove Created Element  **************************************************************** //
// ************************************************** Remove Created Element  **************************************************************** //
  $("#formgroup , #editFormGroup").on('click', "i.clsRemoveElement", function (e) {
    $(this).parents('.removeCreatedElement').remove();
  })

  //});

  //function getSelectedText() {
  //  if (window.getSelection) {
  //    return window.getSelection().toString();
  //  } else if (document.selection) {
  //    return document.selection.createRange().text;
  //  }
  //  return '';
  //}
})

