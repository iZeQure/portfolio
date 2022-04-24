export function setDocumentLangCode() {
    const culture = localStorage.getItem("culture");

    if (culture == null) {
        console.warn("Culture settings was null, setting default language to 'en'.");

        document.documentElement.setAttribute("lang", "en");
        return;
    }

    const languageCode = culture.split("-")[0];

    document.documentElement.setAttribute("lang", languageCode);
}