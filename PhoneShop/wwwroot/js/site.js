console.log("Писька")
var images = document.querySelectorAll("[class*=secondaryimg]");
images.forEach(e => {
    $(e).click(function () {
        //images.forEach(e => {
        //    e.classList.remove("active");
        //})
        //e.classList.add("active");
        $("#mainImage").attr("src", e.getAttribute("src"));
    })
})