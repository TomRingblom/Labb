jQuery.validator.addMethod("name", function (value, element, param) {
    var otherPropId = $(element).data('val-other');
    if (otherPropId !== "#Tom") {
        return true;
    }

    if (value === "Tom") {
        return true;
    } else {
        return false;
    }
    //if (otherPropId) {
    //    var otherProp = $(otherPropId);
    //    if (otherProp) {
    //        var otherVal = otherProp.val();
    //        if (otherVal === 'True') {
    //            return element.checked;
    //        }
    //    }
    //}
    //return false;
});

//jQuery.validator.addMethod("name", function (value, element, param) {
//    if (value != "Tom") {
//        console.log("HMM");
//        return false;
//    }
//    else {
//        console.log("Yay");
//        return true;
//    }
//});

jQuery.validator.unobtrusive.adapters.addBool("name");