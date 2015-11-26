import datetime

current_state = ''
log_file = 'sys.log'
def log_error(msg):
    global log_file
    global current_state
    f = open(log_file, 'a')
    date = datetime.datetime.now().isoformat()
    f.write("Error @ {}\nState: {}\n*******\n{}\n\n".format(date, current_state, msg))
    f.close()

def log_info(msg):
    global log_file
    global current_state
    f = open(log_file, 'a')
    date = datetime.datetime.now().isoformat()
    f.write("Info @ {}\nState: {}\n*******\n{}\n\n".format(date, current_state, msg))
    f.close()
