# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Behavior',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=200)),
            ],
        ),
        migrations.CreateModel(
            name='Node',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('physical_address', models.CharField(max_length=200)),
                ('behavior', models.ForeignKey(to='monitor.Behavior')),
            ],
        ),
        migrations.CreateModel(
            name='Sensor',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('sensor_name', models.CharField(max_length=200)),
                ('measurement_name', models.CharField(max_length=200)),
                ('measurement_unit', models.CharField(max_length=50)),
                ('node', models.ForeignKey(to='monitor.Node')),
            ],
        ),
        migrations.CreateModel(
            name='SensorReading',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('reading', models.IntegerField(default=0)),
                ('reading_date', models.DateTimeField(verbose_name=b'date and time of the reading')),
                ('sensor', models.ForeignKey(to='monitor.Sensor')),
            ],
        ),
    ]
