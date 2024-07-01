import socket
import threading

class Server:
    def __init__(self):
        self.server_socket = None

    def start(self, port):
        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.server_socket.bind(('0.0.0.0', port))
        self.server_socket.listen()

        print(f"Server started on port {port}")

        try:
            while True:
                client_socket, client_address = self.server_socket.accept()
                print(f"Connection from {client_address}")
                client_handler = ClientHandler(client_socket)
                client_handler.start()
        except KeyboardInterrupt:
            self.stop()

    def stop(self):
        if self.server_socket:
            self.server_socket.close()
            print("Server stopped")

class ClientHandler(threading.Thread):
    def __init__(self, client_socket):
        threading.Thread.__init__(self)
        self.client_socket = client_socket

    def run(self):
        try:
            with self.client_socket:
                out = self.client_socket.makefile('w')
                in_ = self.client_socket.makefile('r')

                for input_line in in_:
                    if input_line.strip().lower() == "hello server":
                        out.write("hello client\n")
                    elif input_line.strip().lower() == "bye":
                        out.write("bye\n")
                        break
                    else:
                        out.write("unrecognized message\n")
                    out.flush()
        except Exception as e:
            print(f"Exception in client handler: {e}")

if __name__ == "__main__":
    server = Server()
    server.start(12345)  # Cambia el puerto según tus necesidades
