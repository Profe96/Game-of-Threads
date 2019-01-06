function serializeSchema(form) {
    let array = [].map.call(form.getElementsByTagName("*"), function (el) {
        switch (el.tagName) {
            case 'INPUT':
                switch (el.type) {
                    case 'checkbox':
                        return (el.checked) ? {
                            id: el.id,
                            check: el.checked,
                        } : null
                    default:
                        return {
                            id: el.id,
                            value: el.value,
                        }
                }
        }
    }).filter(function (e) { return e !== undefined; });
    return array.filter(Boolean);
}