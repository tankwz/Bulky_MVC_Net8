
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
    var shorttext = document.getElementById("short-des-" + productId);

    var button = document.getElementById("readmorebutton-" + productId);
    if (extratext.style.display === "none") {
        extratext.style.display = "inline";
        shorttext.style.display = "none";
        //document.getElementById("readmorelink-" + productId).innerHTML = "Show less";
        button.textContent = "Hide Description";
    }
    else {
        extratext.style.display = "none";
        shorttext.style.display = "none";

        //  document.getElementById("readmorelink-" + productId).innerHTML = "Show More";
        button.textContent = "Show Description";

    }
}
const clickableDivs = document.querySelectorAll('.clickable');

const TotalBasePrice = document.getElementById('TotalBasePrice');
const TotalOrderPrice = document.getElementById('TotalOrderPrice');
var CalculatedBasePrice = 0;
var CalculatedOrderPrice = 0;

clickableDivs.forEach(function (div) {
    // let isGreen = false;
    div.addEventListener('click', function () {
        const productId = div.id.split('-')[1]; // Extract the product ID from the div id
        var selectBox = document.getElementById('selected-' + productId)

        var BasePrice = document.getElementById('BasePrice-' + productId).textContent;
        BasePrice = parseFloat(BasePrice.replace(/[^0-9.]/g, ''));
        var OrderPrice = document.getElementById('OrderPrice-' + productId).textContent;
        OrderPrice = parseFloat(OrderPrice.replace(/[^0-9.]/g,''));
        var QuantityCount = document.getElementById('Count-' + productId).textContent;

        QuantityCount = parseFloat(QuantityCount);

     //   console.log('BasePrice:', BasePrice);
     //   console.log('OrderPrice:', OrderPrice);
     //   console.log('QuantityCount:', QuantityCount);
        if (selectBox.checked == true) {
            div.style.border = '3px solid grey';
            selectBox.checked = false;
            selectAll.checked = false;
            CalculatedBasePrice -= BasePrice * QuantityCount;
         //   const formattedPrice = price.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
         //   console.log(formattedPrice)
            TotalBasePrice.innerHTML = CalculatedBasePrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
            CalculatedOrderPrice -= OrderPrice * QuantityCount;
            TotalOrderPrice.innerHTML = '&nbsp;' + CalculatedOrderPrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
        } else {
            div.style.border = '3px solid green';
            selectBox.checked = true;
            CalculatedBasePrice += BasePrice * QuantityCount;
            TotalBasePrice.innerHTML = CalculatedBasePrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
            CalculatedOrderPrice += OrderPrice * QuantityCount;
            TotalOrderPrice.innerHTML = '&nbsp;' + CalculatedOrderPrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
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

        var BasePrice = document.getElementById('BasePrice-' + productId).textContent;
        BasePrice = parseFloat(BasePrice.replace(/[^0-9.]/g, ''));
        var OrderPrice = document.getElementById('OrderPrice-' + productId).textContent;
        OrderPrice = parseFloat(OrderPrice.replace(/[^0-9.]/g, ''));
        var QuantityCount = document.getElementById('Count-' + productId).textContent;


        if (selectAll.checked) {
            div.style.border = '3px solid green '
            if(selectBox.checked == false)
            {
                CalculatedBasePrice += BasePrice * QuantityCount;
                TotalBasePrice.innerHTML = CalculatedBasePrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
                CalculatedOrderPrice += OrderPrice * QuantityCount;
                TotalOrderPrice.innerHTML = '&nbsp;' + CalculatedOrderPrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });

                selectBox.checked = true;
            }

            
            
        }
        else if (!selectAll.checked) {
            div.style.border = '3px solid grey';    
            selectBox.checked = false;
            CalculatedBasePrice = 0;
            TotalBasePrice.innerHTML = CalculatedBasePrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
            CalculatedOrderPrice = 0;
            TotalOrderPrice.innerHTML = '&nbsp;' + CalculatedOrderPrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
        }
    })

})

