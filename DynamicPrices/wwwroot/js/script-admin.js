function toggleSidebar() {
    document.querySelector('.page-content').classList.toggle('sidebar-open');
    document.querySelector('.page-content').parentElement.classList.toggle('sidebar-open');
}

document.addEventListener('DOMContentLoaded', () => {
    const sidebarToggleBtn = document.querySelector('.sidebar-btn');
    sidebarToggleBtn.addEventListener('click', toggleSidebar);
})


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

function manage(category) {
    if (document.querySelector('.options').classList.contains('d-none')) {
        document.querySelector('.options').classList.remove('d-none');
    }
    document.getElementById('add-electronice').classList.add('d-none');
    document.getElementById('mod-electronice').classList.add('d-none');
    switch (category) {
        case 'electronice':
            if (document.getElementById('add-electronice').classList.contains('d-none') && document.getElementById('mod-electronice').classList.contains('d-none')) {
                document.getElementById('add-electronice').classList.remove('d-none');
                document.getElementById('mod-electronice').classList.remove('d-none');
            }

            fetch('/Admin/AllProduseElectronice')
                .then(Response => Response.json())
                .then(data => {
                    const table_info = document.querySelector('.table-info');
                    table_info.innerHTML = '';
                    if (data && data.length > 0) {
                        const table = document.createElement('table');
                        table.className = 'table table-dark table-sm table-striped';
                        const thead = document.createElement('thead');
                        thead.className = 'custom-header';
                        thead.innerHTML = `
                            <tr>
                                <th scope="col">Id</th>
                                <th scope="col">Nume Produs</th>
                                <th scope="col">Tip Produs</th>
                                <th scope="col">Cost Producere</th>
                                <th scope="col">Preț Recomandat</th>
                                <th scope="col">Preț Curent</th>
                                <th scope="col">Descriere</th>
                            </tr>
                        `;
                        table.appendChild(thead);
                        const tbody = document.createElement('tbody');
                        data.forEach(produs => {
                            const tr = document.createElement('tr');
                            tr.classList.add('table-secondary')
                            tr.innerHTML = `
                                <td>${produs['IdProdus']}</td>
                                <td>${produs['NumeProdus']}</td>
                                <td>${produs['TipProdus']}</td>
                                <td>${produs['CostProducere'].toFixed(2)}</td>
                                <td>${produs['PretRecomandat'].toFixed(2)}</td>
                                <td>${produs['PretCurent'].toFixed(2)}</td>
                                <td>${produs['Descriere']}</td>     
                            `;
                            tbody.appendChild(tr);
                        });
                        table.appendChild(tbody);
                        table_info.appendChild(table);
                    } else {
                        table_info.innerHTML = '<p>Niciun produs disponibil.</p>';
                    }
                })
                .catch(error => {
                    console.error('Eroare: ', error);
                });
            break;
    }
}

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