import socket

class Client:
    def __init__(self):
        self.client_socket = None
        self.out = None
        self.in_ = None

    def start_connection(self, ip, port):
        self.client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.client_socket.connect((ip, port))
        self.out = self.client_socket.makefile('w')
        self.in_ = self.client_socket.makefile('r')

    def send_message(self, msg):
        self.out.write(msg + '\n')
        self.out.flush()
        return self.in_.readline().strip()

    def stop_connection(self):
        if self.in_:
            self.in_.close()
        if self.out:
            self.out.close()
        if self.client_socket:
            self.client_socket.close()

if __name__ == "__main__":
    client = Client()
    client.start_connection("127.0.0.1", 12345)  # Cambia la IP y el puerto según tus necesidades

    try:
        response = client.send_message("hello server")
        print(f"Server response: {response}")

        response = client.send_message("bye")
        print(f"Server response: {response}")
    finally:
        client.stop_connection()
