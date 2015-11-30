import time, datetime

prev_line = "\033[F\033[F\033[F\033[F\033[F\033[F\033[F\033[F\033[F\033[F"

__time_now__ = ""
__env_temp__ = ""
__env_hum__ = ""
__env_lum__ = ""
__soil_temp__ = ""
__soil_moist__ = ""
__server_resp__ = ""
__node_updating__ = ""
__last_error__ = ""


info = "\n=================================\n"
info += "Current Time: {}\n"
info += "Env: Temp.: {}C  Humidity: {}%  &  Luminosity:{}%\n"
info += "Soil: Temp.: {}C  &  Moisture: {}%\n\n"
info += "Node Update: {}\n"
info += "Last Server Response: {}\n"
info += "Last Error: {}\n"
info += prev_line


def update(reload = True, env_temp = None, env_hum = None, env_lum = None, soil_temp = None, soil_moist = None, server_resp = None, node_updating = None, last_error = None):
    global __time_now__, __env_temp__, __env_hum__, __env_lum__, __soil_temp__, __soil_moist__, __server_resp__, __node_updating__, __last_error__

    if env_temp is not None:
        __env_temp__ = env_temp
    if env_hum is not None:
        __env_hum__ = env_hum
    if env_lum is not None:
        __env_lum__ = env_lum
    if soil_temp is not None:
        __soil_temp__ = soil_temp
    if soil_moist is not None:
        __soil_moist__ = soil_moist
    if server_resp is not None:
        __server_resp__ = server_resp
    if node_updating is not None:
        __node_updating__ = node_updating
    if last_error is not None:
        __last_error__ = last_error

    if reload:
        __time_now__ = datetime.datetime.now().isoformat()
        print(info.format(__time_now__, __env_temp__, __env_hum__, __env_lum__, __soil_temp__, __soil_moist__, __node_updating__, __server_resp__, __last_error__))
