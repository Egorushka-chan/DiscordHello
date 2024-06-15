/**
 * @name AccepterPlugin
 * @author Egorushka-chan
 * @description It can write message as your's account, that you can send throw another code via HTTP port
 * @version 0.0.1
 */


const fs = require("fs");
const crypto = require("crypto");
const mySettings = {
    secret: "change it, security",
    buffer_path: "S:\\repos\\Betterdiscord"
}

const even = n => !(n % 2);

const input_buffer = {
    server: "",
    body: ""
}

const pluginName = "AccepterPlugin";
var int;
var int2 = () =>{
    window.alert("daddy");
}

module.exports = class AccepterPlugin{
    constructor(meta){
        
    }
    start(){
        Object.assign(mySettings, BdApi.Data.load(pluginName, "settings"));
        const msgActions = global.ZeresPluginLibrary.DiscordModules.MessageActions;
        const sel_channels = global.ZeresPluginLibrary.DiscordModules.SelectedChannelStore;
        
        int = setInterval(this.readFile, 750);
    
        // fs.writeFileSync(mySettings.buffer_path + "\\test.txt", Object.getOwnPropertyNames(sel_channels).join("\n"));

        // global.ZeresPluginLibrary.DiscordModules.MessageStore.addChangeListener(int2);

        // const checkMethods = {
        //     1: msgActions.sendMessage,
        //     2: msgActions._sendMessage,
        //     3: msgActions.sendGreetMessage
        // };
        // for (const [key, value] of Object.entries(checkMethods)) {
        //     fs.writeFileSync(mySettings.buffer_path + "\\test.txt", value.name+"\n");
        //     fs.writeFileSync(mySettings.buffer_path + "\\test.txt", Object.getOwnPropertyNames(value).join("\n"));
        //     fs.writeFileSync(mySettings.buffer_path + "\\test.txt", "-----\n");
        // };
    }

    stop(){
        clearInterval(int);
        // global.ZeresPluginLibrary.DiscordModules.MessageStore.removeChangeListener(int2);
    }

    getSettingsPanel(){
        const mySettingsPanel = document.createElement("div");
        mySettingsPanel.id = "my-settings";

        const lastPort = mySettings["port"]

        const portInput = this.createSettingInPanel("Connection port", "port", "number", mySettings.port);

        const secretInput = this.createSettingInPanel("Secret", "secret", "text", mySettings.secret);

        const bufferInput = this.createSettingInPanel("Buffer Path", "buffer_path", "text", mySettings.buffer_path);

        mySettingsPanel.append(portInput, secretInput);

        return mySettingsPanel;
    }

    createSettingInPanel(text, key, type, value){
        const setting = Object.assign(document.createElement("div"), {className:"setting"});
        const label = Object.assign(document.createElement("span"), {textContent: text});
        const input = Object.assign(document.createElement("input"), {type:type, name:key, value:value});
        input.addEventListener("change", () => {
            mySettings[key] = input.value;
            BdApi.Data.save(pluginName, "settings", mySettings)
        });
        setting.append(label, input);
        return setting;
    }

    readFile(){
        const seconds =  Math.floor(Date.now())/1000;
        if(even(seconds)){
            const data = fs.readFileSync(mySettings.buffer_path + "\\input_buffer.json");
            input_buffer = JSON.parse(data);
            if(input_buffer['server'] && input_buffer['body']){
                msgActions.sendMessage(input_buffer['server'], { // "343400746714136576"
                    content: input_buffer['body']
                });
            }
            else{
                fs.writeFileSync(mySettings.buffer_path + "\\input_buffer.json", "")
            }
        }

    }
};