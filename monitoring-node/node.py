import sys, os, json

physical_addess = ''
node_info = None

def getMacAddress():
    if sys.platform == 'win32':
        for line in os.popen("ipconfig /all"):
            if line.lstrip().startswith('Physical Address'):
                mac = line.split(':')[1].strip().replace('-',':')
                break
            else:
                for line in os.popen("/sbin/ifconfig"):
                    if line.find('Ether') > -1:
                        mac = line.split()[4]
                        break
    return mac

def get():
    global physical_addess
    global node_info
    if not physical_addess:
        physical_addess = getMacAddress()

    f = open('node.json', 'r')
    node_info = json.loads(f.read())
    f.close()

    node_info['physicalAddress'] = physical_addess

    return node_info

def update(node):
    node_info = node
    f = open('node.json', 'w')
    serialized = json.dumps(node, sort_keys=True, indent=4, separators=(',', ': '))
    f.write(serialized)
    f.close()
    return 1
