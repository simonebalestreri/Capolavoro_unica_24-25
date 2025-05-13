async function fetchModelData(vehicleId) {
    const url = `https://api.mercedes-benz.com/configurator/v2/markets/de_DE/models/${vehicleId}?zipCode=2000`;
    const apiKey = "0f0c20a4-9d57-4fa1-8602-1b0799504475"; 

    // Mostra il popup con "Caricamento..."
    document.getElementById('popupOverlay').style.display = 'block';
    document.getElementById('popup').style.display = 'block';
    document.getElementById('popupContent').innerHTML = "<p>Caricamento...</p>";

    try {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "x-api-key": apiKey
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        
        // Estraggo solo le informazioni essenziali
        const modelName = data.name || "N/A";
        const price = data.priceInformation?.price ? data.priceInformation.price.toFixed(2) + " €" : "Non disponibile";
        const vehicleType = data.vehicleBody?.bodyName || "N/A";
        const vehicleClass = data.vehicleClass?.className || "N/A";
        const steeringPosition = data.steeringPosition === "LEFT" ? "Sinistra" : "Destra";

        // Mostra i dati nel popup
        document.getElementById('popupContent').innerHTML = `
          <p><strong>Modello:</strong> ${modelName}</p>
          <p><strong>Prezzo:</strong> ${price}</p>
          <p><strong>Tipo di veicolo:</strong> ${vehicleType}</p>
          <p><strong>Classe:</strong> ${vehicleClass}</p>
          <p><strong>Posizione del volante:</strong> ${steeringPosition}</p>
        `;
    } catch (error) {
        document.getElementById('popupContent').innerHTML = "<p>Errore nel caricamento dei dati.</p>";
        console.error("Error fetching data:", error);
    }
}

function closePopup() {
    document.getElementById('popupOverlay').style.display = 'none';
    document.getElementById('popup').style.display = 'none';
}