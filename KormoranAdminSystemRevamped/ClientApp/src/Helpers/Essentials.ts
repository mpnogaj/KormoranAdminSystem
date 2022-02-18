export function isInIFrame() {
	if(window == null) return false;
	return window != window.top;
}

export function binsearch<T>(array: Array<T>, predicate: (item: T) => number){
	var l = 0, p = array.length - 1;
	while(l < p){
		const sr = Math.floor((l + p) / 2);
		const verdict = predicate(array[sr]);
		if(verdict == 0) return sr;
		else if(verdict < 0) l = sr + 1;
		else p = sr - 1;
	}
	if(predicate(array[l]) != 0) return -1;
	return l;
}