class CookieManager {

	public static getCookie(name: string): string | null {
		const v = document.cookie.match("(^|;) ?" + name + "=([^;]*)(;|$)");
		return v ? v[2] : null;
	}

	public static setCookie(name: string, value: string): void {
		document.cookie = name + "=" + value + ";path=/;";
	}

	public static deleteCookie(name: string): void { 
		CookieManager.setCookie(name, "");
	}
}

export default CookieManager;