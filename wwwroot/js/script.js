// Obter o valor de uma variável CSS pelo seu nome
window.getCssVar = (name) => {
    return getComputedStyle(document.body)
        .getPropertyValue(name)
        .trim();
};

let displaySpecsCard = true;
let displayCpuCard = true;
let displayRamCard = true;
let displayStorageCard = true;
let displayNetCard = true;

window.setDisplaySpecsCard = (bool) => {
    displaySpecsCard = bool;
}

window.getDisplaySpecsCard = () => {
    return displaySpecsCard;
}

window.setDisplayCpuCard = (bool) => {
    displayCpuCard = bool;
}

window.getDisplayCpuCard = () => {
    return displayCpuCard;
}

window.setDisplayRamCard = (bool) => {
    displayRamCard = bool;
}

window.getDisplayRamCard = () => {
    return displayRamCard;
}

window.setDisplayStorageCard = (bool) => {
    displayStorageCard = bool;
}

window.getDisplayStorageCard = () => {
    return displayStorageCard;
}

window.setDisplayNetCard = (bool) => {
    displayNetCard = bool;
}

window.getDisplayNetCard = () => {
    return displayNetCard;
}

window.toggleDisplaySpecsCard = () => {
    displaySpecsCard = !displaySpecsCard;
}

window.toggleDisplayCpuCard = () => {
    displayCpuCard = !displayCpuCard;
}

window.toggleDisplayRamCard = () => {
    displayRamCard = !displayRamCard;
}

window.toggleDisplayStorageCard = () => {
    displayStorageCard = !displayStorageCard;
}

window.toggleDisplayNetCard = () => {
    displayNetCard = !displayNetCard;
}

// Lista de temas disponíveis
const availableThemesList = [
    {cssClassName:"theme-light",name:"Claro"},
    {cssClassName:"theme-light-warm",name:"Ensolarado"},
    {cssClassName:"theme-dark",name:"Escuro"},
    {cssClassName:"theme-solarized-dark",name:"Escuro Ensolarado"},
    {cssClassName:"theme-ocean",name:"Oceano"},
    {cssClassName:"theme-dracula",name:"Drácula"},
    {cssClassName:"theme-rustic",name:"Rústico"},
    {cssClassName:"theme-cyber",name:"Cyber"},
    {cssClassName:"theme-steampunk",name:"Steampunk"},
    {cssClassName:"theme-cyberpunk",name:"Cyberpunk"}
];

// Índice do tema atual
let currentThemeIndex = 0;

// Usar o próximo tema disponível
window.useNextTheme = () => {
    removeBodyClass(availableThemesList[currentThemeIndex].cssClassName);
    currentThemeIndex =
        (currentThemeIndex + 1) %
        availableThemesList.length;
    addBodyClass(availableThemesList[currentThemeIndex].cssClassName);
    //updateCurrentThemeLabel();
}

// Atualizar o texto que indica o tema atual
//window.updateCurrentThemeLabel = () => {
//    const el = document.getElementById("current-theme-label");
//    el.textContent = `Tema: ${availableThemesList[currentThemeIndex].name}`;
//}

// Getter e setter de currentThemeIndex
window.getCurrentThemeIndex = () => {
    return currentThemeIndex;
};
window.setCurrentThemeIndex = (newThemeIndex) => {
    currentThemeIndex = newThemeIndex;
}

// Lista de fontes
const availableFontsList = [
    {
        cssClassName: "font-default",
        name: "Default"
    },
    {
        cssClassName: "font-quicksand",
        name: "Quicksand"
    },
    {
        cssClassName: "font-kode-mono",
        name: "Kode Mono"
    },
    {
        cssClassName: "font-montserrat",
        name: "Montserrat"
    },
    {
        cssClassName: "font-merriweather",
        name: "Merriweather"
    },
    {
        cssClassName: "font-fira-code",
        name: "Fira Code"
    },
    {
        cssClassName: "font-times-new-roman",
        name: "Times New Roman"
    },
    {
        cssClassName: "font-press-start-2p",
        name: "Press Start 2P"
    },
    {
        cssClassName: "font-tomorrow",
        name: "Tomorrow"
    },
    {
        cssClassName: "font-orbitron",
        name: "Orbitron"
    }
];

// Índice da fonte atual
let currentFontIndex = 0;

// Usar a próxima fonte disponível
window.useNextFont = () => {
    removeBodyClass(
        availableFontsList[currentFontIndex].cssClassName
    );

    currentFontIndex =
        (currentFontIndex + 1) %
        availableFontsList.length;

    addBodyClass(
        availableFontsList[currentFontIndex].cssClassName
    );
//    updateCurrentFontLabel();
};

// Atualiza o texto que indica a fonte atual
//window.updateCurrentFontLabel = () => {
//    const el = document.getElementById("current-font-label");
//    el.textContent = `Fonte: ${availableFontsList[currentFontIndex].name}`;
//}

// Getter e setter de currentFontIndex
window.getCurrentFontIndex = () => {
    return currentFontIndex;
};
window.setCurrentFontIndex = (newFontIndex) => {
    currentFontIndex = newFontIndex;
}

// Aplicar preferências do usuário
window.applyUserPreferences = (prefThemeIndex, prefFontIndex, prefDisplaySpecsCard, prefDisplayCpuCard, prefDisplayRamCard, prefDisplayStorageCard, prefDisplayNetCard) =>{
    // Remover tema antigo
    removeBodyClass(availableThemesList[currentThemeIndex].cssClassName);
    
    // Remover fonte antiga
    removeBodyClass(availableFontsList[currentFontIndex].cssClassName);
    
    // Novo tema
    currentThemeIndex = prefThemeIndex;
    addBodyClass(availableThemesList[prefThemeIndex].cssClassName);
    //updateCurrentThemeLabel();
    
    // Nova fonte
    currentFontIndex = prefFontIndex;
    addBodyClass(availableFontsList[prefFontIndex].cssClassName);
    
    setDisplaySpecsCard(prefDisplaySpecsCard);
    setDisplayCpuCard(prefDisplayCpuCard);
    setDisplayRamCard(prefDisplayRamCard);
    setDisplayStorageCard(prefDisplayStorageCard);
    setDisplayNetCard(prefDisplayNetCard);
    
    updateCardDisplaying();
}

window.updateCardDisplaying = () => {
    document.getElementById("card-specs").style.display =
        displaySpecsCard ? "flex" : "none";

    document.getElementById("card-cpu").style.display =
        displayCpuCard ? "flex" : "none";

    document.getElementById("card-ram").style.display =
        displayRamCard ? "flex" : "none";

    document.getElementById("card-storage").style.display =
        displayStorageCard ? "flex" : "none";
    
    document.getElementById("card-net").style.display =
        displayNetCard ? "flex" : "none";
}

// Atualizar o gráfico de uso de armazenamento
window.updateStorageChart = (fillPercent, fillColorVarName) => {
const el = document.getElementById('storage-pie-chart');
if (!el) return;

const bgColor = getCssVar("--color-background");
const fillColor = getCssVar(fillColorVarName);

const p = Math.max(0, Math.min(100, fillPercent));

el.style.background = `conic-gradient(
    ${fillColor} 0% ${p}%,
    ${bgColor} ${p}% 100%
)`;
};

// Atualizar os demais gráficos
window.updateChart = (chartFillDivId, fillColorVarName, fillPercent) => {
const maxWidth = 260;
const chartFill = document.getElementById(chartFillDivId);
if (!chartFill) return;

chartFill.style.background = `${getCssVar(fillColorVarName)}`;
const fillWidth = fillPercent*maxWidth/100;
chartFill.style.width = `${fillWidth}px`;
}

// Adiciona uma classe CSS no body
window.addBodyClass = (className) => {
document.body.classList.add(className);
};

// Remover uma classe CSS do body
window.removeBodyClass = (className) => {
document.body.classList.remove(className);
};
