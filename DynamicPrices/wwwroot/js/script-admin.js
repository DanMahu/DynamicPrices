function toggleSidebar() {
    document.querySelector('.page-content').classList.toggle('sidebar-open');
    document.querySelector('.page-content').parentElement.classList.toggle('sidebar-open');
}

document.addEventListener('DOMContentLoaded', () => {
    const sidebarToggleBtn = document.querySelector('.sidebar-btn');
    sidebarToggleBtn.addEventListener('click', toggleSidebar);
})

function searchProduct() {
    var input, filter, table, tr, td;
    input = document.getElementById('search-product');
    filter = input.value.toUpperCase();
    table = document.querySelector('.table');
    tr = table.getElementsByTagName('tr');

    for (var i = 1; i < tr.length; i++) {
        td = tr[i].getElementsByTagName('td')[1];
        if (td) {
            var txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = '';
            } else {
                tr[i].style.display = 'none';
            }
        }
    }
}

/*
<div class="d-flex p-0">
<button type="button" onclick="goBackToSelect()" id="revertSelect" class="btn btn-primary form-control ms-2 d-none" style="max-width: 3em; border-radius: 10px; padding: 0"><i class="bi bi-arrow-counterclockwise" style="font-size: 24px"></i></button>
<select asp-for="TipProdus" class="form-select py-2" id="select-type" onchange="handleSelectChange(this)"></select>
</div>                                      
 
function handleSelectChange(selectElement) {
    if (selectElement.value === 'new-type') {
        document.getElementById('select-type').classList.add('d-none');
        document.getElementById('input-new-type').classList.remove('d-none');
        document.getElementById('revertSelect').classList.remove('d-none');
    }
}

function goBackToSelect() {
    document.getElementById('input-new-type').classList.add('d-none');
    document.getElementById('revertSelect').classList.add('d-none');
    document.getElementById('select-type').classList.remove('d-none');
}
*/

function showProductTypes() {
    fetch('/Admin/TipProduse')
        .then(Response => Response.json())
        .then(data => {
            //alert(JSON.stringify(data));
            const select = document.getElementById('select-type');
            select.innerHTML = '<option selected disabled>Selectează</option>';
            if (data && data.length > 0) {
                data.forEach(element => {
                    const option = document.createElement('option');
                    option.value = element.tip_produs;
                    option.innerHTML = element.tip_produs;
                    select.appendChild(option);
                });
                const altTip = document.createElement('option');
                altTip.value = 'new-type';
                altTip.innerHTML = 'Tip nou';
                select.appendChild(altTip);
            } else {
                const altTip = document.createElement('option');
                altTip.value = 'new-type';
                altTip.innerHTML = 'Tip nou';
                select.appendChild(altTip);
            }
        })
        .catch(error => {
            console.error('Eroare: ', error);
        });
}

function addProduct(category) {
    const page = document.querySelector('.page-content');
    switch (category) {
        case 'electronice':
            fetch('/Admin/TipProduse')
                .then(Response => Response.json())
                .then(data => {
                    //alert(JSON.stringify(data));
                    const select = document.getElementById('select-type');
                    select.innerHTML = '<option selected disabled>Selectează</option>';
                    if (data && data.length > 0) {
                        data.forEach(element => {
                            const option = document.createElement('option');
                            option.value = element.tip_produs;
                            option.innerHTML = element.tip_produs;
                            select.appendChild(option);
                        });
                        const altTip = document.createElement('option');
                        altTip.value = 'new-type';
                        altTip.innerHTML = 'Tip nou';
                        select.appendChild(altTip);
                    } else {
                        const altTip = document.createElement('option');
                        altTip.value = 'new-type';
                        altTip.innerHTML = 'Tip nou';
                        select.appendChild(altTip);
                    }
                })
                .catch(error => {
                    console.error('Eroare: ', error);
                });
            break;
    }
}