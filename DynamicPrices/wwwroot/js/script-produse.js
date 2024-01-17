function showDetails(div_id) {
    document.getElementById(div_id).classList.toggle("show");
    document.getElementById(div_id).parentElement.classList.toggle("open");
}

function showMenu(menu_id) {
    const allMenus = document.querySelectorAll('.dropdown-tip-produse');
    allMenus.forEach(function (menu) {
        if (menu.id === menu_id) {
            menu.style.display = 'block';
        }
    })
}

function hideAltMenu() {
    const allMenus = document.querySelectorAll('.dropdown-tip-produse');
    allMenus.forEach(function (menu) {
        menu.style.display = 'none';
    })
}

function showProductsByType(tipProdus) {
    fetch('/DB/ProduseElectronice?tipProdus=' + tipProdus)
        .then(Response => Response.json())
        .then(data => {
            const container = document.querySelector('.container-info');
            container.innerHTML = '';

            if (data && data.length > 0) {
                const table = document.createElement('table');
                table.className = 'table table-dark table-sm table-striped';
                const thead = document.createElement('thead');
                thead.className = 'custom-header';
                thead.innerHTML = `
                    <tr>
                        <th scope="col">Nr.</th>
                        <th scope="col">Nume Produs</th>
                        <th scope="col">Cost Producere</th>
                        <th scope="col">Preț Recomandat</th>
                        <th scope="col">Descriere</th>
                        <th scope="col">Preț Curent</th>
                    </tr>
                `;
                table.appendChild(thead);
                const tbody = document.createElement('tbody');
                let nr = 1;
                data.forEach(produs => {
                    const tr = document.createElement('tr');
                    tr.classList.add('table-secondary')
                    tr.innerHTML = `
                        <td>${nr}</td>
                        <td>${produs['NumeProdus']}</td>
                        <td>${produs['CostProducere'].toFixed(2)}</td>
                        <td>${produs['PretRecomandat'].toFixed(2)}</td>
                        <td>${produs['Descriere']}</td>
                        <td>${produs['PretCurent'].toFixed(2)}</td>
                    `;
                    nr++;
                    tbody.appendChild(tr);
                });
                table.appendChild(tbody);
                container.appendChild(table);
            } else {
                container.innerHTML = '<p>Niciun produs disponibil.</p>';
            }
        })
        .catch(error => {
            console.error('Eroare: ', error);
        });

    //afisare meniu de istoric preturi
    showMenu('detalii-produse');
    fetch('ListaDeProduse?tipProdus=' + tipProdus)
        .then(Response => Response.json())
        .then(data => {
            //alert(JSON.stringify(data));
            const div_istoric = document.querySelector('.detalii-dropdown-content');
            div_istoric.innerHTML = '';
            div_istoric.innerHTML = `<input type="text" class="search-bar" id="search-bar1" placeholder="Caută produs..." oninput="searchProducts('search-bar1')">`;
            if (data && data.length > 0) {
                data.forEach(produs => {
                    const a = document.createElement('a');
                    a.href = '#' + produs.id_produs;
                    a.classList.add('product-item');
                    a.onclick = function () {
                        showOptions(produs.id_produs, produs.nume_produs);
                    };
                    a.innerHTML = produs.nume_produs;
                    div_istoric.appendChild(a);
                });
            } else {
                div_istoric.innerHTML = "<a>-EROARE-</a>";
            }
        })
        .catch(error => {
            console.error('Eroare: ', error);
        });
}

function searchProducts(search_id) {
    const searchTerm = document.getElementById(search_id).value.toLowerCase();
    const productItems = document.querySelectorAll('.product-item');

    productItems.forEach(item => {
        const productName = item.innerHTML.toLowerCase();

        if (productName.includes(searchTerm)) {
            item.style.display = 'block';
        } else {
            item.style.display = 'none';
        }
    });
}

function showPriceHistory(product_id, product_name) {
    fetch('/DB/IstoriePreturiDupaProdus?product_id=' + product_id)
        .then(Response => Response.json())
        .then(data => {
            const container = document.querySelector('.container-info');
            container.innerHTML = '';

            if (data && data.length > 0) {
                const table = document.createElement('table');
                table.className = 'tabel-istoric table table-dark table-sm table-striped';

                const thead = document.createElement('thead');
                thead.className = 'custom-header2';
                thead.innerHTML = `
                    <tr>
                        <th colspan="3">${product_name}</th>
                    </tr>
                    <tr>
                        <th>Preț Vechi</th>
                        <th>Preț Nou</th>
                        <th>Dată Modificare</th>
                    </tr>
                `;
                table.appendChild(thead);
                const tbody = document.createElement('tbody');
                data.forEach(produs => {
                    const tr = document.createElement('tr');
                    tr.classList.add('table-secondary');
                    tr.innerHTML = `
                        <td>${produs['pret_vechi'].toFixed(2)}</td>
                        <td>${produs['pret_nou'].toFixed(2)}</td>
                        <td>${produs['data_modificare']}</td>
                    `;
                    tbody.appendChild(tr);
                });
                table.appendChild(tbody);
                container.appendChild(table);
            } else {
                container.innerHTML = '<p class="else-message">Acest produs nu a avut modificări de preț.</p>';
            }
        })
        .catch(error => {
            console.error('Eroare: ', error);
        });
}

function showMinMaxStoc(idProdus, numeProdus) {
    fetch('MinMaxStoc?idProdus=' + idProdus)
        .then(Response => Response.json())
        .then(data => {
            const container = document.querySelector('.container-info');
            container.innerHTML = '';

            if (data && data.length > 0) {
                const table = document.createElement('table');
                table.className = 'tabel-stoc table table-dark table-sm table-striped';

                const thead = document.createElement('thead');
                thead.className = 'custom-header3';
                thead.innerHTML = `
                    <tr>
                        <th colspan="3">${numeProdus}</th>
                    </tr>
                    <tr>
                        <th>În Stoc</th>
                        <th>Stoc Minim</th>
                        <th>Stoc Maxim</th>
                    </tr>
                `;
                table.appendChild(thead);
                const tbody = document.createElement('tbody');
                const tr = document.createElement('tr');
                tr.classList.add('table-secondary');
                tr.innerHTML = `
                    <td>${data[0]}</td>
                    <td>${data[1]}</td>
                    <td>${data[2]}</td>
                `;
                tbody.appendChild(tr);
                table.appendChild(tbody);
                container.appendChild(table);
            } else { container.innerHTML = '<p class="else-message">Acest produs nu a are stoc.</p>'; }
        })
        .catch(error => {
            console.error('Eroare: ' + error);
        });
}

function fetchData(url) {
    return fetch(url)
        .then(Response => {
            if (!Response.ok) {
                throw new Error('Cererea catre server a esuat!');
            }
            return Response.json();
        })
        .catch(error => {
            console.error('Eroare: ', error);
            return null;
        });
}

let lastId = undefined;
function showOptions(idProdus, numeProdus) {
    if (lastId != idProdus) {
        lastId = idProdus;
        const options = document.getElementById('multiple-options');
        const div = document.querySelector('.multiple-options');
        div.innerHTML = '';

        if (!options.classList.contains('afisat')) {
            options.classList.add('afisat');
        } else {
            options.classList.remove('afisat');
            setTimeout(() => {
                options.classList.add('afisat');
            }, 500);
        }
        Promise.all([
            fetchData('PretDupaProdus?idProdus=' + idProdus),
            fetchData('StocDupaProdus?idProdus=' + idProdus)
        ])
            .then(([pret, stoc]) => {
                if (pret != null) {
                    const aPret = document.createElement('a');
                    aPret.style = "color: white";
                    aPret.innerHTML = `Preț Curent (${pret.toFixed(2)})`;
                    setTimeout(() => {
                        div.appendChild(aPret);
                    }, 500);
                }

                if (stoc != null) {
                    const aStoc = document.createElement('a');
                    aStoc.href = '#';
                    aStoc.classList.add('a-option');
                    aStoc.onclick = function () {
                        showMinMaxStoc(idProdus, numeProdus);
                    };
                    aStoc.innerHTML = `Stoc (${stoc}) &#11208`;
                    setTimeout(() => {
                        div.appendChild(aStoc);
                    }, 550);
                }

                const istoricPreturi = document.createElement('a');
                istoricPreturi.href = '#';
                istoricPreturi.classList.add('a-option');
                istoricPreturi.onclick = function () {
                    showPriceHistory(idProdus, numeProdus);
                };
                istoricPreturi.innerHTML = 'Istoric Prețuri &#11208';
                setTimeout(() => {
                    div.appendChild(istoricPreturi);
                }, 600);
            });
    }
}

const options = document.getElementById('multiple-options');
var timer;
options.addEventListener('mouseover', function () {
    options.classList.add('afisat');
    clearTimeout(timer);
});

options.addEventListener('mouseleave', function () {
    timer = setTimeout(() => {
        options.classList.remove('afisat');
        options.innerHTML = '';
    }, 900);
    lastId = undefined;
});