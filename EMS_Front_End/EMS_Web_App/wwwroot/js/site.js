let menuicn = document.querySelector(".menuicn");
let nav = document.querySelector(".navcontainer");

menuicn.addEventListener("click", () => {
    nav.classList.toggle("navclose");
});
nav.classList.toggle("navclose");

function AssignData(hId, value) {
    $("#" + hId).val(value);
}
