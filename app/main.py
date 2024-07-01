import threading
import time
from server.server import Server
from client.client import Client

def start_server():
    try:
        server = Server()
        server.start(6666)
    except Exception as e:
        print(f"Server exception: {e}")

if __name__ == "__main__":
    # Start the server in a new thread
    server_thread = threading.Thread(target=start_server)
    server_thread.start()

    # Give the server a moment to start
    time.sleep(1)

    # Start the client
    client = Client()
    try:
        client.start_connection("127.0.0.1", 6666)
        response = client.send_message("hello server")
        print(f"Server response: {response}")

        response = client.send_message("bye")
        print(f"Server response: {response}")
        
        client.stop_connection()
    except Exception as e:
        print(f"Client exception: {e}")
