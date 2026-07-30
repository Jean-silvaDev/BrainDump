// JS Interop para gravação de áudio no navegador via MediaRecorder API

window.brainDumpAudioRecorder = {
    mediaRecorder: null,
    audioChunks: [],

    startRecording: async function () {
        try {
            const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
            this.audioChunks = [];
            
            // Preferência de MIME types suportados
            const options = MediaRecorder.isTypeSupported('audio/webm;codecs=opus')
                ? { mimeType: 'audio/webm;codecs=opus' }
                : MediaRecorder.isTypeSupported('audio/mp4')
                    ? { mimeType: 'audio/mp4' }
                    : {};

            this.mediaRecorder = new MediaRecorder(stream, options);

            this.mediaRecorder.ondataavailable = (event) => {
                if (event.data.size > 0) {
                    this.audioChunks.push(event.data);
                }
            };

            this.mediaRecorder.start(100); // chunk a cada 100ms
            return true;
        } catch (err) {
            console.error("Erro ao acessar o microfone:", err);
            return false;
        }
    },

    stopRecording: function () {
        return new Promise((resolve) => {
            if (!this.mediaRecorder || this.mediaRecorder.state === "inactive") {
                resolve(null);
                return;
            }

            this.mediaRecorder.onstop = async () => {
                const mimeType = this.mediaRecorder.mimeType || "audio/webm";
                const audioBlob = new Blob(this.audioChunks, { type: mimeType });
                
                // Parar todos os tracks do microfone
                this.mediaRecorder.stream.getTracks().forEach(track => track.stop());

                // Converter blob para Uint8Array para enviar para o C# Blazor
                const arrayBuffer = await audioBlob.arrayBuffer();
                const byteArray = new Uint8Array(arrayBuffer);

                resolve({
                    data: Array.from(byteArray),
                    contentType: mimeType,
                    size: audioBlob.size
                });
            };

            this.mediaRecorder.stop();
        });
    },

    cancelRecording: function () {
        if (this.mediaRecorder && this.mediaRecorder.state !== "inactive") {
            this.mediaRecorder.onstop = null;
            this.mediaRecorder.stop();
            if (this.mediaRecorder.stream) {
                this.mediaRecorder.stream.getTracks().forEach(track => track.stop());
            }
        }
        this.audioChunks = [];
    }
};
