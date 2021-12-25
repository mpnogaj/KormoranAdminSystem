export function isInIFrame() {
	if(window == null) return false;
	return window != window.top;
}