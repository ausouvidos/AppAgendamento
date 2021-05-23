import axios from 'axios';
import User from '../models/user.model';

class UserService {
  private loggedInStatus: boolean | null = null;

  public async isAdmin(): Promise<boolean> {
    const url = '/api/Identity/IsAdmin';
    const response = await axios.get<boolean>(url, this.getAuthOptions());
    return response.data;
  }

    public async getUsers(): Promise<User[]> {
        const url = '/api/Identity/Users';
        const response = await axios.get<User[]>(url, this.getAuthOptions());
        return response.data;
    }


  public isLoggedIn() {
    return this.loggedInStatus ? this.loggedInStatus : !!sessionStorage.getItem('msal.idtoken');
  }

  public signIn() {
    return new Promise((resolve, reject) => {
      const script = document.createElement('script');
      script.src = 'https://alcdn.msftauth.net/lib/1.2.1/js/msal.js';
      script.onload = () => {
        this.openSignInPopup(resolve, reject);
      };
      document.head.appendChild(script);
    });
  }

  private async openSignInPopup(resolve: (value?: any) => void, reject: (reason?: any) => void) {
    try {
      const msalConfig = {
        auth: {
          clientId: '4c6c3ac0-a8f7-42f9-8b8e-60b7d829b42d',
          authority: 'https://login.microsoftonline.com/5560e420-5f71-400c-8bbe-e52fae72eb6c',
          redirectUri: new URL(window.location.href).origin + '/signin-oidc',
        },
        cache: {
          cacheLocation: 'sessionStorage',
          storeAuthStateInCookie: false,
        },
      };
      const myMSALObj = new Msal.UserAgentApplication(msalConfig);
      const response = await myMSALObj.loginPopup({ scopes: ['openid'] });
      this.loggedInStatus = true;
      resolve();
    } catch (error) {
      console.error(error);
      reject(error);
    }
  }

  private getAuthOptions() {
    return {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('msal.idtoken')}`,
      },
    };
  }
}

export default new UserService();
