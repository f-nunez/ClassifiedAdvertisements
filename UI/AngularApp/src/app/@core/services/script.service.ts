import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class ScriptService {
  private scripts: { [id: string]: IScript; } = {};

  constructor() {
    scriptList.forEach((script: any) => {
      this.scripts[script.name] = {
        loaded: false,
        name: script.name,
        src: script.src
      };
    });
  }

  public loadScripts(...scripts: string[]) {
    var promises: any[] = [];

    scripts.forEach((script) => promises.push(
      this.loadScript(script)
    ));

    return Promise.all(promises);
  }

  private loadScript(name: string) {
    return new Promise((resolve, reject) => {
      if (this.scripts[name].loaded) {
        resolve({ script: name, loaded: true, status: 'is already loaded' });
      } else {
        let script = document.createElement('script') as any;
        script.type = 'text/javascript';
        script.src = this.scripts[name].src;

        if (script.readyState) {
          // IE browser
          script.onreadystatechange = () => {
            if (script.readyState === "loaded" || script.readyState === "complete") {
              script.onreadystatechange = null;
              this.scripts[name].loaded = true;
              resolve({ script: name, loaded: true, status: 'loaded' });
            }
          };
        } else {
          // Other browsers
          script.onload = () => {
            this.scripts[name].loaded = true;
            resolve({ script: name, loaded: true, status: 'loaded' });
          };
        }

        script.onerror = (error: any) => resolve({
          script: name,
          loaded: false,
          status: 'loaded'
        });

        document.getElementsByTagName('head')[0].appendChild(script);
      }
    });
  }
}

interface IScript {
  loaded: boolean;
  name: string;
  src: string;
}

export const scriptList: IScript[] = [
  { loaded: false, name: 'theme.js', src: '../../../assets/js/theme.js' }
];
