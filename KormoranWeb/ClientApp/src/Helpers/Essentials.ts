export function isInIFrame(): boolean {
    if (window == null) return false;
    return window != window.top;
}

/*
returns index to element that matches given predictate. Predictate function should return:
value < 0 when target item is on the right,
value = 0 when an item meets the criteria,
value > 0 when target item is on the left
when there is no element that meets the criteria function returns -1
*/
export function binsearch<T>(array: Array<T>, predicate: (item: T) => number): T | undefined {
    let l = 0, p = array.length - 1;
    while (l < p) {
        const sr = Math.floor((l + p) / 2);
        const verdict = predicate(array[sr]);
        if (verdict == 0) return array[sr];
        else if (verdict < 0) l = sr + 1;
        else p = sr - 1;
    }
    if (predicate(array[l]) != 0) return undefined;
    return array[l];
}

export function binsearchInd<T>(array: Array<T>, predicate: (item: T) => number): number {
    let l = 0, p = array.length - 1;
    while (l < p) {
        const sr = Math.floor((l + p) / 2);
        const verdict = predicate(array[sr]);
        if (verdict == 0) return sr;
        else if (verdict < 0) l = sr + 1;
        else p = sr - 1;
    }
    if (predicate(array[l]) != 0) return -1;
    return l;
}