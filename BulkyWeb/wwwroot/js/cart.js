
     // function toggleDescription(productId) {
        //     var moreText = document.getElementById("more-" + productId);
        //     if (moreText.style.display === "none" || moreText.style.display === "") {
        //         moreText.style.display = "inline";
        //         document.getElementById("readmorelink-" + productId).innerHTML = "Show less";
        //     } else {
        //         moreText.style.display = "none";
        //         document.getElementById("readmorelink-" + productId).innerHTML = "Show More";
        //     }
        //     return false; // Prevent the default anchor link behavior
        // }
        function toggleDescription(productId) {
            var extratext = document.getElementById("extra-des-" + productId);
            var button = document.getElementById("readmorebutton-" + productId);
            if (extratext.style.display === "none") {
                extratext.style.display = "inline";
                //document.getElementById("readmorelink-" + productId).innerHTML = "Show less";
                button.textContent = "Show Less";
            }
            else {
                extratext.style.display = "none";
                //  document.getElementById("readmorelink-" + productId).innerHTML = "Show More";
                button.textContent = "Show More";

            }
        }
     const clickableDivs = document.querySelectorAll('.clickable');

    clickableDivs.forEach(function (div) {
        // let isGreen = false;
        div.addEventListener('click', function () {
            const productId = div.id.split('-')[1]; // Extract the product ID from the div id
            var selectBox = document.getElementById('selected-' + productId)
            if (selectBox.checked == true) {
                div.style.border = '3px solid grey';
                selectBox.checked = false;
                selectAll.checked = false;
            } else {
                div.style.border = '3px solid green';
                selectBox.checked = true;
            }
            const allChecked = Array.from(clickableDivs).every((div) => {
                const productId = div.id.split('-')[1];
                var selectBox = document.getElementById('selected-' + productId);
                return selectBox.checked;
            });
            selectAll.checked = allChecked;
            //  isGreen = !isGreen;
            //    console.log('Selected Product ID:', productId);
        });
     });

    const selectAll = document.getElementById('SelectAll')
    selectAll.addEventListener('change', function () {

        clickableDivs.forEach(function (div) {
            const productId = div.id.split('-')[1]; // Extract the product ID from the div id
            var selectBox = document.getElementById('selected-' + productId)
            if (selectAll.checked) {
                div.style.border = '3px solid green '
                selectBox.checked = true;
            }
            else if (!selectAll.checked) {
                div.style.border = '3px solid grey';
                selectBox.checked = false;
            }
        })

    })