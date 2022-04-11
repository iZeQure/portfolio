const cultureStorageKey = "culture";

export function getCulture() {
    return localStorage[cultureStorageKey];
};

export function setCulture(value) {
    localStorage[cultureStorageKey] = value;
};