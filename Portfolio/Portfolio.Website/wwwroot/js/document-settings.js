export function setDocumentTitle(title) {
    if (title === undefined) {
        return;
    }

    document.title = title;
}