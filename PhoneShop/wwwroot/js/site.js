var images = document.querySelectorAll("[class*=secondaryimg]");
images.forEach(e => {
    $(e).click(function ()
    {
        $("#mainImage").attr("src", e.getAttribute("src"));
    })
})



