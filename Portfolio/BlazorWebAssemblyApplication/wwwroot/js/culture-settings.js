const cultureStorageKey = "SiteCulture";

export function getCulture() {
    return window.localStorage[cultureStorageKey];
};

export function setCulture(value) {
    window.localStorage[cultureStorageKey] = value;
};