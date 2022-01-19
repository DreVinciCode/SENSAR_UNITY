import sys
import os
import csv
import numpy as np
import matplotlib.pyplot as plt

class visual_toggled():
    def __init__(self, timeStr):
        self._timestamp = float(timeStr[1:])
        self._toggled = True if timeStr[0] == 't' else False
    def get_times(self):
        return self._timestamp
    def is_toggled(self):
        return self._toggled
    def __str__(self):
        return "({}: {})".format(self.is_toggled(), self.get_times())


def make_timeline(labels, timestamps):
    print("making timeline")
    y_offset = len(labels)
    for i in range(0, len(labels)):
        print("{}:".format(labels[i]))
        for stamp in timestamps[i]:
            print("{}".format(stamp), end=', ')
        print("\n", end='')
    

def main():
    if len(sys.argv) != 2:
        print("usage: python timeliner.py [path to visualization CSV]")
        return
    with open(sys.argv[1]) as in_file: 
        in_file = csv.reader(in_file)
        visuals = []
        for row in in_file:
            visuals.append(row)
    metric_names = visuals[0]
    visuals = visuals[1:]
    timestamps = []
    for row in visuals:
        for stamps in row:
            indiv_visuals = []
            stampList = stamps.split(' ')
            for stamp in stampList:
                indiv_visuals.append(visual_toggled(stamp))
            timestamps.append(indiv_visuals)
    make_timeline(metric_names, timestamps)


if __name__ == "__main__":
    main()